using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;
        public CourseRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //************************************** GetAllCourse *****************************************
        public async Task<Course> GetCourseSinglePageInfo(CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                    .Include(x => x.CourseLevel)
                    .Include(x => x.CourseStatus)
                    .Include(x => x.User)
                    .Include(x => x.CourseComments)
                    .Include(x => x.CourseEpisodes)
                    .Include(x => x.CourseGroup)
                    .Include(x => x.SubCourseGroup)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        //************************************** GetAllCourse *****************************************
        public async Task<List<Course>> GetAllCourse(string courseTitle, CancellationToken cancellationTokenn)
        {
            return await _context.Courses
                           .Where(x => x.CourseTitle
                           .Contains(courseTitle))
                           .ToListAsync(cancellationTokenn);
        }

        //************************************** GetLastCourses ****************************************
        public async Task<List<Course>> GetLastCourses(CancellationToken cancellationToken)
        {
            return await _context.Courses
                               .OrderByDescending(x => x.CreateDate)
                               .Take(5)
                               .ToListAsync(cancellationToken);
        }

        //************************************** GetPopularCourses *************************************
        public async Task<List<Course>> GetPopularCourses(CancellationToken cancellationToken)
        {
            return await _context.Courses
                         .Include(x => x.OrderDetails)
                         .Where(x => x.OrderDetails.Any())
                         .OrderByDescending(x => x.OrderDetails.Count())
                       //.OrderByDescending(x => x.ViewCount).Take(5)
                         .Take(8)
                         .ToListAsync(cancellationToken);
        }

        //************************************** ExistCourseName *************************************
        public async Task<bool> ExistCourseName(string courseName, CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                  .Where(x => x.CourseTitle == courseName)
                  .AnyAsync(cancellationToken);

            return result;
        }
    }
}
