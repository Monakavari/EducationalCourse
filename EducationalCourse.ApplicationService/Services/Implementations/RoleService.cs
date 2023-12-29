using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Role;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class RoleService : IRoleService
    {
        #region Constructor

        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleService(IPermissionRepository permissionrepository,
                         IUnitOfWork unitOfWork,
                         IRolePermissionRepository rolePermissionRepository,
                         IHttpContextAccessor httpContextAccessor,
                         IUserRoleRepository userRoleRepository,
                         IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        #endregion Constructor

        //********************************** AddRole *************************************
        public async Task<ApiResult> AddRole(AddRoleDto request, CancellationToken cancellationToken)
        {
            var role = new Role()
            {
                Name = request.Name,
                Title = request.RoleTitle,
                IsActive = true
            };
            await _roleRepository.AddAsync(role, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************** AssignUserRole ******************************
        public async Task<ApiResult> AssignUserRole(AssignUserRoleDto request, CancellationToken cancellationToken)
        {
            var userRoleList = new List<UserRole>();

            foreach (var userId in request.UserIds)
            {
                foreach (var roleId in request.RoleIds)
                {
                    userRoleList.Add(new UserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        IsActive = true
                    });
                }
            }
            await _userRoleRepository.AddRangeAsync(userRoleList, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************** EditRole ************************************
        public async Task<ApiResult> EditRole(EditRoleDto request, CancellationToken cancellationToken)
        {
            var entity = await _roleRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.Name = request.Name;
            entity.Title = request.RoleTitle;
            entity.Name = request.Name;
            entity.IsActive = true;

            _roleRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************** GetAllRoles *********************************
        public async Task<ApiResult<List<RoleDto>>> GetAllRoles(CancellationToken cancellationToken)
        {
            var roleList = await _roleRepository.GetAllRoles(cancellationToken);
            var result = new List<RoleDto>();

            if (roleList is null)
                throw new AppException("نقشی یافت نشد.");

            foreach (var role in roleList)
            {
                result.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    RoleTitle = role.Title,
                    IsActive = true,
                    CreateDateDisplay = PersianDate.ToShamsi(role.CreateDate)
                });
            }
            return new ApiResult<List<RoleDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }
    }
}
