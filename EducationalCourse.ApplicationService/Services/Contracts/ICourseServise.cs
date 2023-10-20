using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseServise
    {
        Task<ApiResult<List<CourseDto>>> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken);
    }
}
