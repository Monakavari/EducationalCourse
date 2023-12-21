using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class OrderController : BaseController
    {
        #region Constructor

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        #endregion Constructor

        [HttpGet]
        public async Task<IActionResult> CreateUserOrder(int userId, int courseId, CancellationToken cancellationToken = default)
        {
            var result = await _orderService.CreateUserOrder(userId, courseId, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderForUserPannel(int userId, int courseId, CancellationToken cancellationToken = default)
        {
            var result = await _orderService.GetOrderForUserPannel(userId, courseId, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserOrders(CancellationToken cancellationToken = default)
        {
            var result = await _orderService.GetUserOrders(cancellationToken);
            return Ok(result);
        }
    }
}
