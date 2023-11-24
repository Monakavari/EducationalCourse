using EducationalCourse.Domain.Dtos.Order;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IOrderService
    {
        #region Order
        Task<ApiResult> CreateUserOrder(int userId,int courseId, CancellationToken cancellationToken);
        Task<ApiResult<GetUserOrderForPannelDto>> GetOrderForUserPannel(int userId,int courseId, CancellationToken cancellationToken);
        
        
        #endregion  Order

        #region Order detail
        //Task<ApiResult> AddOrderDetailToOrder(AddOrderDetailDto request, CancellationToken cancellationToken);

        #endregion Order detail 
    }
}
