using EducationalCourse.Common.Dtos.Role;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleDto>>> GetAllRoles(CancellationToken cancellationToken);

        Task<ApiResult> AddRole(AddRoleDto request, CancellationToken cancellationToken);

        Task<ApiResult> EditRole(EditRoleDto request, CancellationToken cancellationToken);

        Task<ApiResult> AssignUserRole(AssignUserRoleDto request, CancellationToken cancellationToken);

    }
}
