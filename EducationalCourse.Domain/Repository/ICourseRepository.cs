using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<List<FilterCourseDto>> GetAllCourse(string courseTitle, CancellationToken cancellationToken);
        Task<Course> GetCourseSinglePageInfo(CancellationToken cancellationToken);
        Task<List<FilterCourseDto>> GetLastCourses(CancellationToken cancellationToken);
        Task<List<FilterCourseDto>> GetPopularCourses(CancellationToken cancellationToken);
        Task<bool> ExistCourseName(string courseName, CancellationToken cancellationToken);

    }
}
