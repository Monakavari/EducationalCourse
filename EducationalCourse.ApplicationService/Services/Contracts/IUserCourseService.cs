using EducationalCourse.Domain.Entities.Order;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IUserCourseService
    {
        Task CreateUserCourse(List<OrderDetail> orderDetails, int userId, CancellationToken cancellationToken);
    }
}
