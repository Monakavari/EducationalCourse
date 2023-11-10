using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IPrepareForViewService
    {
        Task<ApiResult<PrepareForViewDto>> GetCourseInfoToFillCombos(CancellationToken cancellationToken);

    }
}
