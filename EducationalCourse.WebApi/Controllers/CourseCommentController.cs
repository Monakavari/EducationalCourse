using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Dtos.Course;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class CourseCommentController : BaseController
    {
        #region Constructor

        private readonly ICourseCommentService _commentService;

        public CourseCommentController(ICourseCommentService commentService)
        {
            _commentService = commentService;
        }

        #endregion Constructor
        [HttpPost]
        public async Task<IActionResult> CreateComment(AddCommentDto request, CancellationToken cancellationToken = default)
        {
            var result = await _commentService.CreateComment(request, cancellationToken);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment(int id, CancellationToken cancellationToken = default)
        {
            var result = await _commentService.DeleteComment(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRepliedComment(AddRepliedCommentDto request, CancellationToken cancellationToken = default)
        {
            var result = await _commentService.CreateRepliedComment(request, cancellationToken);
            return Ok(result);
        }

    }
}
