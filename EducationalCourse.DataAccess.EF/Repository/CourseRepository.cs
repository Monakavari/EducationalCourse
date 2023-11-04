using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<FilterCourseDto>> GetAllCourse(string courseTitle, CancellationToken cancellationTokenn)
        {
            var result = await _context.Courses
                       .Where(x => x.CourseTitle.Contains(courseTitle))
                       .Select(x => new FilterCourseDto
                       {
                           CourseTitle = x.CourseTitle,
                           CoursePrice = x.CoursePrice,
                           IsFreeCost = x.IsFreeCost,
                           CourseImageBase64 = x.CourseImageBase64,
                           CourseImageName = x.CourseImageName

                       }).ToListAsync(cancellationTokenn);

            return result;
        }

        //************************************** GetLastCourses ****************************************
        public async Task<List<FilterCourseDto>> GetLastCourses(CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                               .OrderByDescending(x => x.CreateDate).Take(5)
                               .Select(x => new FilterCourseDto
                               {
                                   CourseTitle = x.CourseTitle,
                                   CoursePrice = x.CoursePrice,
                                   IsFreeCost = x.IsFreeCost,
                                   CourseImageBase64 = x.CourseImageBase64,
                                   CourseImageName = x.CourseImageName

                               }).ToListAsync(cancellationToken);

            return result;
        }

        //************************************** GetPopularCourses *************************************
        public async Task<List<FilterCourseDto>> GetPopularCourses(CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                               .OrderByDescending(x => x.ViewCount).Take(5)
                               .Select(x => new FilterCourseDto
                               {
                                   CourseTitle = x.CourseTitle,
                                   CoursePrice = x.CoursePrice,
                                   IsFreeCost = x.IsFreeCost,
                                   CourseImageBase64 = x.CourseImageBase64,
                                   CourseImageName = x.CourseImageName

                               }).ToListAsync(cancellationToken);

            return result;

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
