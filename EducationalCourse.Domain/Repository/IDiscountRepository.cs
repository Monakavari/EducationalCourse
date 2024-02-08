using EducationalCourse.Common.Dtos.Discount;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<Discount> GetDiscountByCode(string code, CancellationToken cancellationToken);
        Task<List<Discount>> GetAllDiscounts(CancellationToken cancellationToken);
        Task<Discount> GetDiscountById(int discountId, CancellationToken cancellationToken);
        Task<bool> IsExistDiscountCode(string discountCode, CancellationToken cancellationToken);
    }
}
