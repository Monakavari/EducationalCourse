using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<CourseDto>> GetAllCourse(string courseTitle, CancellationToken cancellationTokenn)
        {
            var result = await _context.Courses
                       .Where(x => x.CourseTitle.Contains(courseTitle))
                       .Select(x => new CourseDto
                       {
                           CourseTitle = x.CourseTitle,
                           CoursePrice = x.CoursePrice,
                           IsFreeCost = x.IsFreeCost,
                           CourseImageBase64 = x.CourseImageBase64,
                           CourseImageName = x.CourseImageName

                       }).ToListAsync(cancellationTokenn);

            return result;
        }

    }
}
