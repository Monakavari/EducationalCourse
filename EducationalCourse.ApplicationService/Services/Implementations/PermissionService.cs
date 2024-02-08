using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.DTOs.Permission;
using EducationalCourse.Common.Enums;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Entities.Permission;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        #region Constructor

        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PermissionService(IPermissionRepository permissionrepository,
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

        //********************************* AddPermission ***********************************
        public async Task<ApiResult> AddPermission(AddPermissionDto request, CancellationToken cancellationToken)
        {
            var permission = new Permission()
            {
                Name = request.Name,
                Title = request.PermissionTitle,
                IsActive = request.IsActive,
            };
            await _permissionRepository.AddAsync(permission, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************* AssignRolePermission ****************************
        public async Task<ApiResult> AssignRolePermission(AssignRolePermissionDto request, CancellationToken cancellationToken)
        {
            var rolePermissionList = new List<RolePermission>();

            foreach (var roleId in request.RoleIds)
            {
                foreach (var permissionId in request.PermissionIds)
                {
                    rolePermissionList.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permissionId,
                        IsActive = true
                    });
                }
            }
            await _rolePermissionRepository.AddRangeAsync(rolePermissionList, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************* EditPermission **********************************
        public async Task<ApiResult> EditPermission(EditPermissionDto request, CancellationToken cancellationToken)
        {
            var entity = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);

            entity.Name = request.Name;
            entity.Title = request.PermissionTitle;
            entity.IsActive = request.IsActive;
            entity.Log = "Edit Permission";

            _permissionRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************* GetAllPermissions *******************************
        public async Task<ApiResult<List<PermissionDto>>> GetAllPermissions(CancellationToken cancellationToken)
        {
            var entitiesList = await _permissionRepository.GetAllPermission(cancellationToken);
            var result = new List<PermissionDto>();

            foreach (var item in entitiesList)
            {
                result.Add(new PermissionDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    IsActive = true,
                    PermissionTitle = item.Title,
                    CreateDateDisplay = PersianDate.ToShamsi(item.CreateDate)
                });
            }
            return new ApiResult<List<PermissionDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //********************************* GetPermissionIdByCode ***************************
        public async Task<int> GetPermissionIdByCode(PermissionCodeEnum code, CancellationToken cancellationToken)
        {
           return await _permissionRepository.GetPermissionId(code, cancellationToken);
        }

        //********************************* GrantPermission *********************************
        public async Task<bool> GrantPermission(PermissionCodeEnum code, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();
            return await _permissionRepository.HasPermission(userId, code, cancellationToken);
        }
    }
}
