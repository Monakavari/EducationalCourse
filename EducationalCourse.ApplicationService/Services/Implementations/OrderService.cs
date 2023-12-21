using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Dtos.Order;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class OrderService : IOrderService
    {
        #region Constructor

        private readonly IOrderRepository _orderRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static int _userId = 0;
        public OrderService(IOrderRepository orderRepository,
                     IUnitOfWork unitOfWork,
                     IUserRepository userRepository,
                     ICourseRepository courseRepository,
                     IOrderDetailRepository orderDetailRepository,
                     IHttpContextAccessor httpContextAccessor,
                     IWalletRepository walletRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _courseRepository = courseRepository;
            _orderDetailRepository = orderDetailRepository;
            _httpContextAccessor = httpContextAccessor;
            _walletRepository = walletRepository;
            _userId = _httpContextAccessor.GetUserId();
        }

        #endregion Constructor

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

        //******************************************* GetOrderById ********************************************
        public async Task<Order> GetOrderById(int orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderById(orderId, cancellationToken);
            return order;
        }

        //******************************************* GetUserOrders ********************************************
        public async Task<ApiResult<List<GetUserOrdersDto>>> GetUserOrders(CancellationToken cancellationToken)
        {
            var result = new List<GetUserOrdersDto>();

            var orderList = await _orderRepository.GetUserOrders(_userId, cancellationToken);
            if (orderList == null)
                throw new Exception("سفارشی یافت نشد.");

            MappOrder(result, orderList);

            return new ApiResult<List<GetUserOrdersDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");

        }

        private void MappOrder(List<GetUserOrdersDto> result, List<Order> orderList)
        {
            foreach (var order in orderList)
            {
                result.Add(new GetUserOrdersDto
                {
                    PaymentDateDisplay = order.PaymentDate.HasValue ? order.PaymentDate.Value.ToShamsi() : null,
                    TotalPayment = order.TotalPayment,
                    OrderDetails = MappOrderDetails(order)
                });
            };
        }

        private List<OrderDetailDto> MappOrderDetails(Order order)
        {
            var result = new List<OrderDetailDto>();

            foreach (var item in order.OrderDetails)
            {
                result.Add(new OrderDetailDto
                {
                    OrderId = item.OrderId,
                    Count = item.Count,
                    CourseId = item.CourseId,
                    Price = item.Price,
                });
            }

            return result;
        }
    }
}


