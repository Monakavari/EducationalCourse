using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class UserCourseService : IUserCourseService
    {
        #region Constructor

        private readonly IUserCourseRepository _userCourseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserCourseService(IUserCourseRepository userCourseRepository,
                                 IUnitOfWork unitOfWork)
        {
            _userCourseRepository = userCourseRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public async Task CreateUserCourse(List<OrderDetail> orderDetails, int userId, CancellationToken cancellationToken)
        {
            var userCourses = new List<UserCourse>();
            var courseIds = orderDetails.Select(x => x.CourseId).ToList();

            foreach (var courseId in courseIds)
                userCourses.Add(new UserCourse
                {
                    CourseId = courseId,
                    UserId = userId,
                    IsActive = true,
                });

            await _userCourseRepository.AddRangeAsync(userCourses, cancellationToken);
        }

    }
}
