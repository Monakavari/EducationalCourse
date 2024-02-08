using EducationalCourse.Common.Dtos.Discount;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IDiscountService
    {
        Task<ApiResult> UseDiscount(UseDiscountDto request, CancellationToken cancellationToken);
        Task<ApiResult> CreateDiscount(CreateDiscountDto request, CancellationToken cancellationToken);
        Task<ApiResult> UpdateDiscount(UpdateDiscountDto request, CancellationToken cancellationToken);
        Task<ApiResult<List<Discount>>> GetAllDiscounts(CancellationToken cancellationToken);
    }
}
