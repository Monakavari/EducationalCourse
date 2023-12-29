using EducationalCourse.Common.Enums;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Permission;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public PermissionRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //******************************* GetAllAsync ********************************
        public async Task<List<Permission>> GetAllPermission(CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .Where(x => x.IsActive)
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);
        }

        //******************************* GetPermissionId ****************************
        public async Task<int> GetPermissionId(PermissionCodeEnum code, CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .Where(x => x.PermissionCode == code)
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        //******************************* GetUserPermission **************************
        public async Task<bool> GetUserPermission(int userId, int permissionId, CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(x => x.UserId == userId)
                .Include(x => x.Role)
                .ThenInclude(x => x.RolePermissions)
                .Where(x => x.Role.RolePermissions
                      .Any(x => x.PermissionId == permissionId))
                .AnyAsync(cancellationToken);
        }

        //******************************* HasPermission ******************************
        public async Task<bool> HasPermission(PermissionCodeEnum code, CancellationToken cancellationToken)
        {
            return await _context.Permissions
                .Where (x => x.PermissionCode == code)
                .AnyAsync (cancellationToken);
        }

    }
}
