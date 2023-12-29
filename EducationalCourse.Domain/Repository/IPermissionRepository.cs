using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Entities.Permission;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Task<bool> GetUserPermission(int userId, int permissionId, CancellationToken cancellationToken);

        Task<List<Permission>> GetAllPermission(CancellationToken cancellationToken);

        Task<int> GetPermissionId(PermissionCodeEnum code, CancellationToken cancellationToken);

        Task<bool> HasPermission(int userId,PermissionCodeEnum code, CancellationToken cancellationToken);
    }
}
