using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseCommentService
    {
        Task<ApiResult> CreateComment(AddCommentDto request,CancellationToken cancellation);
        Task<ApiResult> CreateRepliedComment(AddRepliedCommentDto request,CancellationToken cancellation);
        Task<ApiResult> DeleteComment(int id,CancellationToken cancellation);
     
    }
}
