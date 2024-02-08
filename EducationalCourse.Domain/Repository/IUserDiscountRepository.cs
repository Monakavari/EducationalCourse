using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IUserDiscountRepository :IBaseRepository<UserDiscount>
    {
        Task<bool> CheckUserDiscount(int userId,int discountId,CancellationToken cancellationToken);
        Task AddUserDiscountUsed(int userId,int discountId,CancellationToken cancellationToken);
    }
}
