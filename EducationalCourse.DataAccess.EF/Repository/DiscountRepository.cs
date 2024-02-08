using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        #region Constructor

        private readonly EducationalCourseContext context;

        public DiscountRepository(EducationalCourseContext context) : base(context)
        {
            this.context = context;
        }

        #endregion Constructor

        //******************************* GetDiscountCode *****************************
        public async Task<Discount> GetDiscountByCode(string code, CancellationToken cancellationToken)
        {
            var doscount = await context.Discounts
                .Where(x => x.DiscountCode == code)
                .SingleOrDefaultAsync(cancellationToken);

            return doscount;
        }

        //******************************* GetAllDiscounts *****************************
        public async Task<List<Discount>> GetAllDiscounts(CancellationToken cancellationToken)
        {
            return await context.Discounts.ToListAsync(cancellationToken);

        }

        //******************************* GetDiscountById *****************************
        public async Task<Discount> GetDiscountById(int discountId, CancellationToken cancellationToken)
        {
            var result = await context.Discounts
                    .Where(x => x.Id == discountId)
                    .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        //******************************* IsExistDiscountCode *************************
        public async Task<bool> IsExistDiscountCode(string discountCode, CancellationToken cancellationToken)
        {
            var result = await context.Discounts
                        .Where(x => x.DiscountCode == discountCode)
                        .AnyAsync(cancellationToken);

            return result;
        }
    }
}
