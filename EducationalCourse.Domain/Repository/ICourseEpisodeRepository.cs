using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface ICourseEpisodeRepository : IBaseRepository<CourseEpisode>
    {
        Task<List<CourseEpisodeDto>> GetAllCourseEpisodeByCourseId(int id, CancellationToken cancellationToken);
        Task<List<CourseEpisode>> GetAllCourseEpisode(int courseId, CancellationToken cancellationToken);
        Task<CourseEpisode> GetById(int id, CancellationToken cancellationToken);
    }
}
