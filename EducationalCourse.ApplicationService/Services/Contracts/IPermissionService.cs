using EducationalCourse.Common.DTOs.Permission;
using EducationalCourse.Common.Enums;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IPermissionService
    {
        Task<ApiResult<List<PermissionDto>>> GetAllPermissions(CancellationToken cancellationToken);

        Task<ApiResult> AddPermission(AddPermissionDto request, CancellationToken cancellationToken);

        Task<ApiResult> EditPermission(EditPermissionDto request, CancellationToken cancellationToken);
        Task<int> GetPermissionIdByCode(PermissionCodeEnum code, CancellationToken cancellationToken);

        Task<bool> GrantPermission(PermissionCodeEnum code, CancellationToken cancellationToken);
        Task<ApiResult> AssignRolePermission(AssignRolePermissionDto request, CancellationToken cancellationToken);

    }
}
