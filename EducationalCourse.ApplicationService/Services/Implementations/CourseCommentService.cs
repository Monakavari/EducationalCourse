using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Comment;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.BasePaging.Dtos;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseCommentService : ICourseCommentService
    {
        #region Constructor

        private readonly ICourseCommentRepository _CommentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private static int _userId = 0;

        public CourseCommentService(ICourseCommentRepository commentRepository,
                                    IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _CommentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _userId = httpContextAccessor.GetUserId();
        }

        #endregion Constructor

        //**************************************** CreateComment ****************************************
        public async Task<ApiResult> CreateComment(AddCommentDto request, CancellationToken cancellationToken)
        {
            //ToDo Validation
            //ToDo businessValidation
            var comment = new CourseComment();
            comment.Text = request.Text;
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userId;
            comment.CourseId = request.CourseId;
            comment.ParentId = null;

            await _CommentRepository.AddAsync(comment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //**************************************** DeleteComment ****************************************
        public async Task<ApiResult> DeleteComment(int id, CancellationToken cancellationToken)
        {
            var comment = await _CommentRepository.GetByIdAsync(id, cancellationToken);
            await DeleteRepliedComment(id, cancellationToken);
            _CommentRepository.Delete(comment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //**************************************** CreateRepliedComment *********************************
        private async Task<ApiResult> DeleteRepliedComment(int id, CancellationToken cancellationToken)
        {
            var deleteRepliedComments = await _CommentRepository
                        .FetchIQueryableEntity()
                        .Where(c => c.ParentId == id)
                        .ToListAsync(cancellationToken);

            foreach (var item in deleteRepliedComments)
                _CommentRepository.Delete(item);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");

        }

        //**************************************** CreateRepliedComment *********************************
        public async Task<ApiResult> CreateRepliedComment(AddRepliedCommentDto request, CancellationToken cancellationToken)
        {
            //ToDo Validation
            //ToDo businessValidation
            var comment = new CourseComment();
            comment.Text = request.Text;
            comment.IsDelete = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userId;
            comment.CourseId = request.CourseId;
            comment.ParentId = request.CommentId;

            await _CommentRepository.AddAsync(comment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }
        //**************************************** GetComments ******************************************
        public async Task<ApiResult<DataGridResult<CourseComment>>> GetComments(GetAllCommentsDto request, CancellationToken cancellation)
        {
            var gridState = new GridState
            {
                Take = request.BasePaging.Take,
                Skip = request.BasePaging.Skip
            };

            var result = await _CommentRepository
                .FetchIQueryableEntity()
                .Include(x => x.User)
                .Where(x => x.ParentId == request.CourseId)
                .Where(x => !x.IsDelete)
                .ApplyPaging(gridState);

            return new ApiResult<DataGridResult<CourseComment>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }
    }
}
