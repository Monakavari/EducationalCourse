using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Role;
using EducationalCourse.Common.DTOs.Permission;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class RoleController : BaseController
    {
        #region Constructor

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #endregion Constructor

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleDto request, CancellationToken cancellationToken = default)
        {
            var result = await _roleService.AddRole(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleDto request, CancellationToken cancellationToken = default)
        {
            var result = await _roleService.AssignUserRole(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> EditRole(EditRoleDto request, CancellationToken cancellationToken = default)
        {
            var result = await _roleService.EditRole(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken = default)
        {
            var result = await _roleService.GetAllRoles(cancellationToken);
            return Ok(result);
        }
    }
}
