﻿using EducationalCourse.Common.Dtos.Comment;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class CourseCommentRepository : BaseRepository<CourseComment>, ICourseCommentRepository
    {
        #region Consteructor

        private readonly EducationalCourseContext _context;
        public CourseCommentRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Consteructor

    }
}
