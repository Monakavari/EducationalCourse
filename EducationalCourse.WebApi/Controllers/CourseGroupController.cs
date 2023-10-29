using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.ApplicationService.Services.Implementations;
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
    }
}
