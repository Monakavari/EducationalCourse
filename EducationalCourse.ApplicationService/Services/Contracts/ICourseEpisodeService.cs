using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseEpisodeService
    {
        Task<ApiResult<List<CourseEpisodeDto>>> GetAllCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken);

        Task<ApiResult> CreateCourseEpisode(List<AddVideoFileCourseDto> request, CancellationToken cancellationToken);

        Task<ApiResult> DeleteCourseEpisode(int id, CancellationToken cancellationToken);

        Task DeleteCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken);

        Task<ApiResult> EditCourseEpisode(List<EditCourseEpisodeDto> request, CancellationToken cancellationToken);
    }
}
