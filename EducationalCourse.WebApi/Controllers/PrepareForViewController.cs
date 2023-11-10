using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class PrepareForViewController : BaseController
    {
        #region Constructor

        private readonly IPrepareForViewService _prepareForViewService;

        public PrepareForViewController(IPrepareForViewService prepareForViewService)
        {
            _prepareForViewService = prepareForViewService;
        }

        #endregion Constructor

        [HttpGet]
        public async Task<IActionResult> GetCourseInfoToFillCombos(CancellationToken cancellationToken = default)
        {
            var result = await _prepareForViewService.GetCourseInfoToFillCombos(cancellationToken);
            return Ok(result);
        }
    }
}
