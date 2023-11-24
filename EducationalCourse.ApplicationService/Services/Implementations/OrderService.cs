using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Dtos.Order;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region Constructor

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        public OrderService(IOrderRepository orderRepository,
                     IUnitOfWork unitOfWork,
                     IUserRepository userRepository,
                     ICourseRepository courseRepository,
                     IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        #endregion Constructor

        #region Order

        //*******************************************CreateUserOrder************************************************
        public async Task<ApiResult> CreateUserOrder(int userId, int courseId, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);
            var order = await _orderRepository.GetUserOpenOrder(userId, cancellationToken);

            if (order is null)
            {
                await CreateBasketOrder(userId, course, cancellationToken);
                return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
            }

            await AddOrderDetailToOrder(order, course, cancellationToken);
            order.TotalPayment += course.CoursePrice;
            order.UpdateDate = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");

        }

        private async Task<OrderDetail> AddOrderDetailToOrder(Order order, Course course, CancellationToken cancellationToken)
        {
            var orderDetail = new OrderDetail()
            {
                OrderId = order.Id,
                CourseId = course.Id,
                Price = course.CoursePrice,
                Count = 1,
                IsActive = true,
            };

            if (await _orderDetailRepository.IsExistOrderDetail(order.Id, course.Id, cancellationToken))
                throw new Exception("دوره تکراری می باشد.");

            await _orderDetailRepository.AddAsync(orderDetail, cancellationToken);
            return orderDetail;
        }

        private async Task<Order> CreateBasketOrder(int userId, Course course, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                UserId = userId,
                IsFinally = false,
                TotalPayment = course.CoursePrice,
                IsActive = true,
                OrderDetails = new List<OrderDetail>()
                {
                    new OrderDetail()
                    {
                        CourseId = course.Id,
                        Price = course.CoursePrice,
                        Count = 1,
                        IsActive = true,
                    }
                }
            };

            await _orderRepository.AddAsync(order, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return order;
        }

        //*******************************************GetOrderForUserPannel********************************************
        public async Task<ApiResult<GetUserOrderForPannelDto>> GetOrderForUserPannel(int userId, int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderForUserPannel(userId, orderId, cancellationToken);

            if (order == null)
                throw new Exception("سفارشی یافت نشد.");

            var result = new GetUserOrderForPannelDto
            {
                PaymentDateDisplay = order.PaymentDate.HasValue ? order.PaymentDate.Value.ToShamsi() : null,
                TotalPayment = order.TotalPayment,
            };

            foreach (var orderDetail in order.OrderDetails)
            {
                result.OrderDetails.Add(new OrderDetailDto
                {
                    Price = orderDetail.Price,
                    OrderId = orderDetail.OrderId,
                    CourseId = orderDetail.CourseId,
                    Count = orderDetail.Count,
                });
            }

            return new ApiResult<GetUserOrderForPannelDto>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد");
        }


        #endregion  Order

        #region Order detail


        #endregion Order detail
    }
}
