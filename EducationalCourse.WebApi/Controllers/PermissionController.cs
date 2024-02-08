using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Common.DTOs.Permission;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class PermissionController : BaseController
    {
        #region Constructor

        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        #endregion Constructor

        [HttpPost]
        public async Task<IActionResult> AddPermission(AddPermissionDto request, CancellationToken cancellationToken = default)
        {
            var result = await _permissionService.AddPermission(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRolePermission(AssignRolePermissionDto request, CancellationToken cancellationToken = default)
        {
            var result = await _permissionService.AssignRolePermission(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditPermission(EditPermissionDto request, CancellationToken cancellationToken = default)
        {
            var result = await _permissionService.EditPermission(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPermissions(CancellationToken cancellationToken = default)
        {
            var result = await _permissionService.GetAllPermissions(cancellationToken);
            return Ok(result);
        }
    }
}
