using Azure.Core;
using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class CourseGroupController : BaseController
    {
        #region Constructor

        private readonly ICourseGroupService _courseGroupService;

        public CourseGroupController(ICourseGroupService courseGroupService)
        {
            _courseGroupService = courseGroupService;
        }

        #endregion Constructor

        [HttpGet]
        public async Task<IActionResult> GetAllCourseGroups(CancellationToken cancellationToken = default)
        {
            var result = await _courseGroupService.GetAllCourseGroups(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddParent(AddParentCourseGroupDto request, CancellationToken cancellationToken = default)
        {
            var result = await _courseGroupService.AddParent(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddChild(AddChildCourseGroupDto request, CancellationToken cancellationToken = default)
        {
            var result = await _courseGroupService.AddChild(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteParent(int id, CancellationToken cancellationToken = default)
        {
            var result = await _courseGroupService.DeleteParent(id, cancellationToken);
            return Ok(result);
        }
    }
}
