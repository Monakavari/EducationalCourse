using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.Domain.Entities;
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCourseEpisode([FromForm] List<AddVideoFileCourseDto> request, CancellationToken cancellationToken = default)
        {
            var result = await _courseEpisodeService.CreateCourseEpisode(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourseEpisode(int courseEpisodeId, CancellationToken cancellationToken = default)
        {
            var result = await _courseEpisodeService.DeleteCourseEpisode(courseEpisodeId, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourseEpisodeByCourseId(int courseId, CancellationToken cancellationToken = default)
        {
            var result = await _courseEpisodeService.GetAllCourseEpisodeByCourseId(courseId, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditCourseEpisode([FromForm] List<EditCourseEpisodeDto> request, CancellationToken cancellationToken = default)
        {
            var result = await _courseEpisodeService.EditCourseEpisode(request, cancellationToken);
            return Ok(result);
        }
    }
}
