using EducationalCourse.Domain.Dtos.Order;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IOrderService
    {
        Task<ApiResult> CreateUserOrder(int userId, int courseId, CancellationToken cancellationToken);
        Task<ApiResult<GetUserOrderForPannelDto>> GetOrderForUserPannel(int userId, int courseId, CancellationToken cancellationToken);
        Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken);
        Task<ApiResult<List<GetUserOrdersDto>>> GetUserOrders(CancellationToken cancellationToken);

    }
}
