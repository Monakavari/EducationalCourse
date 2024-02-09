using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<List<Course>> GetAllCourse(string courseTitle, CancellationToken cancellationToken);
        Task<Course> GetCourseSinglePageInfo(CancellationToken cancellationToken);
        Task<List<Course>> GetLastCourses(CancellationToken cancellationToken);
        Task<List<Course>> GetPopularCourses(CancellationToken cancellationToken);
        Task<bool> ExistCourseName(string courseName, CancellationToken cancellationToken);

    }
}
