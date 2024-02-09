using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class CourseVoteController : BaseController
    {
        #region Constructor

        private readonly ICourseVoteService _courseVoteService;

        public CourseVoteController(ICourseVoteService courseVoteService)
        {
            _courseVoteService = courseVoteService;
        }

        #endregion Constructor

        [HttpPost]
        public async Task<IActionResult> CreateVote(AddUserVoteDto request request, CancellationToken cancellationToken = default)
        {
            var result = await _courseVoteService.CreateVote(request, cancellationToken);
            return Ok(result);
        }
    }
}
