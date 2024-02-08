using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class UserDiscountRepository : BaseRepository<UserDiscount>, IUserDiscountRepository
    {
        #region Consteructor

        private readonly EducationalCourseContext _context;
        public UserDiscountRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Consteructor

        public async Task<bool> CheckUserDiscount(int userId, int discountId, CancellationToken cancellationToken)
        {
            return await _context.UserDiscounts
                             .Where(x => x.UserId == userId && x.DiscountId == discountId)
                             .AnyAsync(cancellationToken);
        }

        public async Task AddUserDiscountUsed(int userId, int discountId, CancellationToken cancellationToken)
        {
            var result = await _context.UserDiscounts
                                 .AddAsync(new UserDiscount()
                                 {
                                     UserId = userId,
                                     DiscountId = discountId
                                 });
        }
    }
}
