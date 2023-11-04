using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class CourseEpisodeController : BaseController
    {
        #region Constructor

        private readonly ICourseEpisodeService _courseEpisodeService;
        public CourseEpisodeController(ICourseEpisodeService courseEpisodeService)
        {
            _courseEpisodeService = courseEpisodeService;
        }

        #endregion Constructor

        //[HttpPost]
        //public async Task<IActionResult> EditCourse(EditCourseDto request, CancellationToken cancellationToken = default)
        //{
        //    var result = await _courseServise.EditCourse(request, cancellationToken);
        //    return Ok(result);
        //}
    }
}
