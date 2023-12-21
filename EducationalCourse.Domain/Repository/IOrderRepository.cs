using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Course> GetCoureseForUserOrder(int courseId, CancellationToken cancellationToken);
        Task<Order> GetUserOpenOrder(int userId, CancellationToken cancellationToken);
        Task<Order> GetOrderForUserPannel(int userId, int orderId, CancellationToken cancellationToken);
        Task<Order> GetUserOrder(int userId, int orderId, CancellationToken cancellationToken);
        Task <List<Order>> GetUserOrders(int userId, CancellationToken cancellationToken);
        Task<Order> GetOrderById(int orderId,CancellationToken cancellationToken);
    }
}
