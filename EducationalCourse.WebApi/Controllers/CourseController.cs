using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class CourseController : BaseController
    {
        #region Constructor

        private readonly ICourseServise _courseServise;
        public CourseController(ICourseServise courseServise)
        {
            _courseServise = courseServise;
        }

        #endregion Constructor

        [HttpGet]
        public async Task<IActionResult> SearchCourseByTitle(string courseTitle, CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.SearchCourseByTitle(courseTitle, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLastCourses(CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.GetLastCourses(cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPopularCourses(CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.GetLastCourses(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetFilterCourses(FilterCourseRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.GetFilterCourses(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCourse([FromForm] AddCourseDto request, CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.CreateCourse(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCourse([FromForm] EditCourseDto request, CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.EditCourse(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id, CancellationToken cancellationToken = default)
        {
            var result = await _courseServise.Delete(id, cancellationToken);
            return Ok(result);
        }
    }
}
