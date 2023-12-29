using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Role;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class RoleService : IRoleService
    {
        #region Constructor

        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleService(IPermissionRepository permissionrepository,
                         IUnitOfWork unitOfWork,
                         IRolePermissionRepository rolePermissionRepository,
                         IHttpContextAccessor httpContextAccessor)
        {
            _permissionRepository = permissionrepository;
            _unitOfWork = unitOfWork;
            _rolePermissionRepository = rolePermissionRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion Constructor

        //********************************** AddRole *************************************
        public Task<ApiResult> AddRole(AddRoleDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //********************************** AssignUserRole ******************************
        public Task<ApiResult> AssignUserRole(AssignUserRoleDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //********************************** EditRole ************************************
        public Task<ApiResult> EditRole(EditRoleDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //********************************** GetAllRoles *********************************
        public Task<ApiResult<List<RoleDto>>> GetAllRoles(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
