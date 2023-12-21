using EducationalCourse.Common.Enums;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public WalletRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //************************************GetUserWallet*******************************
        public async Task<List<Wallet>> GetUserWallet(int userId, CancellationToken cancellationToken)
        {
            var userWalletList = await _context.Wallets
                    .Where(x => x.UserId == userId)
                    .Include(x => x.WalletTransactions)
                    .ToListAsync(cancellationToken);

            return userWalletList;
        }

        //************************************GetWalletByUserId***************************
        public async Task<Wallet> GetWalletByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.Wallets
                    .Where(x => x.UserId == userId)
                    .Where(x => x.IsActive)
                    .Include(x => x.WalletTransactions)
                    .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
