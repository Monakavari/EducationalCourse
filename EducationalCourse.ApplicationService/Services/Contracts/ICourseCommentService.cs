using EducationalCourse.Common.Dtos.Comment;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Framework;
using EducationalCourse.Framework.BasePaging.Dtos;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface ICourseCommentService
    {
        Task<ApiResult> CreateComment(AddCommentDto request,CancellationToken cancellation);
        Task<ApiResult> CreateRepliedComment(AddRepliedCommentDto request,CancellationToken cancellation);
        Task<ApiResult> DeleteComment(int id,CancellationToken cancellation);
        Task<ApiResult<DataGridResult<CourseComment>>> GetComments(GetAllCommentsDto request, CancellationToken cancellation);
     
    }
}
