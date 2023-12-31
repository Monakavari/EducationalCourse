﻿using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
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

        //**********************ExistCourseGroupName***********************************
        public async Task<bool> ExistCourseGroupName(string courseGroupName, CancellationToken cancellationToken)
        {
            var result = await _context.CourseGroups
                .Where(x => x.CourseGroupTitle == courseGroupName)
                .AnyAsync(cancellationToken);

            return result;
        }

        //**********************ExistCourseGroupNameForAnotherCourseGroup**************
        public async Task<bool> ExistCourseGroupNameForAnotherCourseGroup(string courseGroupName, int courseGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.CourseGroups
                    .Where(x => x.CourseGroupTitle == courseGroupName &&
                                    x.Id != courseGroupId)
                    .AnyAsync(cancellationToken);

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
            var result = await _context.CourseGroups
                    .Where(x => x.Id == courseGroupId)
                    .AnyAsync(cancellationToken);

            return result;
        }

        //**********************************HasRelatedCourse****************************
        public async Task<bool> HasRelatedCourse(int courseGroupId, CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                .Where(x => x.Id == courseGroupId)
                .AnyAsync(cancellationToken);

            return result;
        }


        //**********************DecrementParentDirectChildCount************************
        //public async Task DecrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken)
        //{
        //    var courseGroup = await _context.CourseGroups
        //                            .Where(x => x.Id == courseGroupId)
        //                            .FirstOrDefaultAsync(cancellationToken);

        //    if (courseGroup.ParentId != null)
        //    {
        //        var parent = await _context.CourseGroups
        //                           .Where(x => x.Id == courseGroup.ParentId.Value)
        //                           .FirstOrDefaultAsync(cancellationToken);

        //        parent.DirectChildCount = parent.DirectChildCount - 1;

        //    }
        //}

        //***********GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren********
        //public async Task<bool> GenerateAndSetLineageOnUpdateParentIdForNodeAndItsChildren(int courseGroupId, int? newParentID, CancellationToken cancellationToken)
        //{
        //    //var node = db.Categories.FirstOrDefault(x => x.CategoryID == CategorydID);
        //    //string newParentLineage = "";
        //    //if (newParentID != null)
        //    //{

        //    //    newParentLineage = db.Categories.FirstOrDefault(x => x.CategoryID == newParentID).Lineage;
        //    //}
        //    //var nl = node.Lineage;


        //    //var allChildren = db.Categories.Where(x => x.Lineage.StartsWith(nl)).ToList();
        //    //foreach (Category child in allChildren)
        //    //{
        //    //    child.Lineage = child.Lineage.Replace(nl, newParentLineage + CategorydID + ",");
        //    //}
        //    //var newParent = db.Categories.FirstOrDefault(x => x.CategoryID == newParentID);
        //    //if (newParentID != null && newParent != null)
        //    //{
        //    //    newParent.DirectChildCount = newParent.DirectChildCount + 1;

        //    //}
        //    //node.ParentID = newParentID;
        //    //db.SaveChanges();
        //    //return true;
        //    throw new NotImplementedException();
        //}

        //*********************************GenerateLinage******************************
        //public async Task<string> GenerateLinage(int courseGroupId, CancellationToken cancellationToken)
        //{
        //    var courseGroup = await _context.CourseGroups
        //                          .Where(x => x.Id == courseGroupId)
        //                          .FirstOrDefaultAsync(cancellationToken);

        //    var linage = "";

        //    if (courseGroup.ParentId != null)
        //    {
        //        var parent = await _context.CourseGroups
        //                          .Where(x => x.Id == courseGroup.ParentId.Value)
        //                          .FirstOrDefaultAsync(cancellationToken);

        //        linage = courseGroup.Id + courseGroup.ParentId + ",";
        //    }
        //    else
        //        linage = courseGroup.Id + ",";

        //    return linage;
        //}

        ////*********************************GetAllChildren******************************
        //public async Task<List<CourseGroup>> GetAllChildren(int parentId, CancellationToken cancellationToken)
        //{
        //    var courseGroup = await _context.CourseGroups.Where(x => x.Id == parentId).FirstOrDefaultAsync(cancellationToken);
        //    var result = await _context.CourseGroups.Where(x=>x.Lineage.StartsWith(courseGroup.Parent.Lineage)).ToListAsync(cancellationToken);

        //    return result;
        //}
        //**************************IncrementParentDirectChildCount*********************
        // public async Task IncrementParentDirectChildCount(int courseGroupId, CancellationToken cancellationToken)
        //{
        //    var courseGroup = await _context.CourseGroups
        //                            .Where(x => x.Id == courseGroupId)
        //                            .FirstOrDefaultAsync(cancellationToken);

        //    if (courseGroup.ParentId != null)
        //    {
        //        var parent = await _context.CourseGroups
        //                           .Where(x => x.Id == courseGroup.ParentId.Value)
        //                           .FirstOrDefaultAsync(cancellationToken);

        //        parent.DirectChildCount = parent.DirectChildCount + 1;

        //    }
        //}

        //*************************************SetLinage********************************
        //public async Task SetLinage(string linage, int courseGroupId, CancellationToken cancellationToken)
        //{
        //    var courseGroup = await _context.CourseGroups
        //                            .Where(x => x.Id == courseGroupId)
        //                            .FirstOrDefaultAsync(cancellationToken);

        //    if (courseGroup != null)
        //        courseGroup.Lineage = linage;
        //}
    }
}
