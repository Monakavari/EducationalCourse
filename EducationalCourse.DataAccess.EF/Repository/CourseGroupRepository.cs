using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseGroupRepository : BaseRepository<CourseGroup>, ICourseGroupRepository
    {

        #region Constructor

        private readonly EducationalCourseContext _context;
        public CourseGroupRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //**********************DecrementParentDirectChildCount************************
        public async Task DecrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken)
        {
            var courseGroup = await _context.CourseGroups
                                    .Where(x => x.Id == courseGroupId)
                                    .FirstOrDefaultAsync(cancellationToken);

            if (courseGroup.ParentId != null)
            {
                var parent = await _context.CourseGroups
                                   .Where(x => x.Id == courseGroup.ParentId.Value)
                                   .FirstOrDefaultAsync(cancellationToken);

                parent.DirectChildCount = parent.DirectChildCount - 1;

            }
        }

        //**********************ExistCourseGroupName***********************************
        public async Task<bool> ExistCourseGroupName(string courseGroupName, CancellationToken cancellationToken)
        {
            var result = await _context.CourseGroups
                              .AnyAsync(x => x.CourseGroupTitle == courseGroupName, cancellationToken);

            return result;
        }

        //**********************ExistCourseGroupNameForAnotherCourseGroup**************
        public async Task<bool> ExistCourseGroupNameForAnotherCourseGroup(string courseGroupName, int courseGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.CourseGroups
                               .AnyAsync(x => x.CourseGroupTitle == courseGroupName &&
                                              x.Id != courseGroupId, cancellationToken);

            return result;
        }

        //***********GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren********
        public async Task<bool> GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren(int courseGroupId, int? newParentID, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //*********************************GenerateLinage******************************
        public async Task<string> GenerateLinage(int courseGroupId, CancellationToken cancellationToken)
        {
            var courseGroup = await _context.CourseGroups
                                  .Where(x => x.Id == courseGroupId)
                                  .FirstOrDefaultAsync(cancellationToken);

            var linage = "";

            if (courseGroup.ParentId != null)
            {
                var parent = await _context.CourseGroups
                                  .Where(x => x.Id == courseGroup.ParentId.Value)
                                  .FirstOrDefaultAsync(cancellationToken);

                linage = courseGroup.Id + courseGroup.ParentId + ",";
            }
            else
                linage = courseGroup.Id + ",";

            return linage;
        }

        //*********************************GetAllChildren******************************
        public async Task<List<CourseGroup>> GetAllChildren(int parentId, CancellationToken cancellationToken)
        {
            var courseGroup = await _context.CourseGroups.Where(x => x.Id == parentId).FirstOrDefaultAsync(cancellationToken);
            var result = await _context.CourseGroups.Where(x=>x.Lineage.StartsWith(courseGroup.Parent.Lineage)).ToListAsync(cancellationToken);

            return result;
        }

        //*************************************GetCourseGroups**************************
        public async Task<List<CourseGroup>> GetCourseGroups()
        {
            var result = await _context.CourseGroups.ToListAsync();

            return result;
        }

        //**********************************HasChildCourseGroup*************************
        public async Task<bool> HasChildCourseGroup(int courseGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.CourseGroups.AnyAsync(x => x.Id == courseGroupId, cancellationToken);
            return result;
        }

        //**********************************HasRelatedCourse****************************
        public async Task<bool> HasRelatedCourse(int courseGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.Courses.AnyAsync(x => x.Id == courseGroupId, cancellationToken);
            return result;
        }

        //**************************IncrementParentDirectChildCount*********************
        public async Task IncrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken)
        {
            var courseGroup = await _context.CourseGroups
                                    .Where(x => x.Id == courseGroupId)
                                    .FirstOrDefaultAsync(cancellationToken);

            if (courseGroup.ParentId != null)
            {
                var parent = await _context.CourseGroups
                                   .Where(x => x.Id == courseGroup.ParentId.Value)
                                   .FirstOrDefaultAsync(cancellationToken);

                parent.DirectChildCount = parent.DirectChildCount + 1;

            }
        }

        //*************************************SetLinage********************************
        public async Task SetLinage(string linage, int courseGroupId, CancellationToken cancellationToken)
        {
            var courseGroup = await _context.CourseGroups
                                    .Where(x => x.Id == courseGroupId)
                                    .FirstOrDefaultAsync(cancellationToken);

            if (courseGroup != null)
                courseGroup.Lineage = linage;
        }
    }
}
