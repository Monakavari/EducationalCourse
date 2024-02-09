using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseVoteService
    {
        Task<ApiResult> CreateVote(AddUserVoteDto request ,CancellationToken cancellationToken);
    }
}
