using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseGroupService
    {
        Task<ApiResult<List<CourseGroupDto>>> GetAllCourseGroups(CancellationToken cancellationToken);

        Task<ApiResult> AddParent(AddParentCourseGroupDto request, CancellationToken cancellationToken);

        Task<ApiResult> AddChild(AddChildCourseGroupDto request, CancellationToken cancellationToken);

        Task<ApiResult> DeleteParent(int id, CancellationToken cancellationToken);

    }
}
