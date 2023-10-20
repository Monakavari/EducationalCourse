using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Models.Course;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseRepository :IBaseRepository<Course>
    {
        Task<List<CourseDto>> GetAllCourse(string courseTitle,CancellationToken cancellationToken);
    }
}
