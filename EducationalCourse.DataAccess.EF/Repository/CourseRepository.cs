using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
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

        //**************************************GetAllCourse*****************************************
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

        public async Task<List<CourseDto>> GetLastCourses(CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                               .OrderByDescending(x => x.CreateDate).Take(5)
                               .Select(x => new CourseDto
                               {
                                   CourseTitle = x.CourseTitle,
                                   CoursePrice = x.CoursePrice,
                                   IsFreeCost = x.IsFreeCost,
                                   CourseImageBase64 = x.CourseImageBase64,
                                   CourseImageName = x.CourseImageName

                               }).ToListAsync(cancellationToken);

            return result;
        }

        public async Task<List<CourseDto>> GetPopularCourses(CancellationToken cancellationToken)
        {
            var result = await _context.Courses
                               .OrderByDescending(x => x.ViewCount).Take(5)
                               .Select(x => new CourseDto
                               {
                                   CourseTitle = x.CourseTitle,
                                   CoursePrice = x.CoursePrice,
                                   IsFreeCost = x.IsFreeCost,
                                   CourseImageBase64 = x.CourseImageBase64,
                                   CourseImageName = x.CourseImageName

                               }).ToListAsync(cancellationToken);

            return result;

        }
    }
}
