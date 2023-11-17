using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseCommentService : ICourseCommentService
    {
        #region Constructor

        private readonly ICourseCommentRepository _CommentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CourseCommentService(ICourseCommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _CommentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        //**************************************** CreateComment ****************************************
        public async Task<ApiResult> CreateComment(AddCommentDto request, CancellationToken cancellationToken)
        {
            //ToDo Validation
            //ToDo businessValidation
            var comment = new CourseComment();
            comment.Text = request.Text;
            comment.UserId = request.UserId;
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
            _CommentRepository.Delete(comment);
            await DeleteRepliedComment(id, cancellationToken);
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
            comment.UserId = request.UserId;
            comment.CourseId = request.CourseId;
            comment.ParentId = request.CommentId;

            await _CommentRepository.AddAsync(comment, cancellationToken);
            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ثبت نظر در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");

        }

    }
}
