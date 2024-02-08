using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Discount;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class DiscountController : BaseController
    {
        #region Constructor

        private readonly IDiscountService _discountServiceService;
        public DiscountController(IDiscountService discountServiceService)
        {
            _discountServiceService = discountServiceService;
        }

        #endregion Constructor

        [HttpPost]
        public async Task<IActionResult> UseDiscount(UseDiscountDto request, CancellationToken cancellationToken = default)
        {
            var result = await _discountServiceService.UseDiscount(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto request, CancellationToken cancellationToken = default)
        {
            var result = await _discountServiceService.CreateDiscount(request, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto request, CancellationToken cancellationToken = default)
        {
            var result = await _discountServiceService.UpdateDiscount(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts(CancellationToken cancellationToken = default)
        {
            var result = await _discountServiceService.GetAllDiscounts(cancellationToken);
            return Ok(result);
        }
    }
}
