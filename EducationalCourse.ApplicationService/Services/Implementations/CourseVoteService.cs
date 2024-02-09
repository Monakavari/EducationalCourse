using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Course;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class CourseVoteService : ICourseVoteService
    {
        #region Constructor

        private readonly ICourseVoteRepository _courseVoteRepository;
        private readonly IUnitOfWork _unitOfWork;
        private static int _userId = 0;

        public CourseVoteService(ICourseVoteRepository courseVoteRepository,
                                    IUnitOfWork unitOfWork,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _courseVoteRepository = courseVoteRepository;
            _unitOfWork = unitOfWork;
            _userId = httpContextAccessor.GetUserId();
        }

        #endregion Constructor

        //************************************* CreateVote ***************************************
        public async Task<ApiResult> CreateVote(AddUserVoteDto request, CancellationToken cancellationToken)
        {
            var userVote = await _courseVoteRepository.GetUserVote(_userId, request.CourseId, cancellationToken);
            CheckUserVote(request, userVote);

            await _courseVoteRepository.AddAsync(userVote, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        private CourseVote CheckUserVote(AddUserVoteDto request, CourseVote? userVote)
        {
            if (userVote != null)
            {
                userVote.Vote = request.Vote;
            }
            else
            {
                userVote = new CourseVote()
                {
                    UserId = _userId,
                    CourseId = request.CourseId,
                    Vote = request.Vote
                };
            }

            return userVote;
        }
    }
}
