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

        //************************************Deposit*************************************
        public async Task<List<int>> Deposit(int userId, CancellationToken cancellationToken)
        {
            var deposit = await _context.Wallets
                    .Where(x => x.UserId == userId)
                    .Where(x => x.IsPay)
                    .Select(x => x.Amount)
                    .ToListAsync(cancellationToken);

            return deposit;
        }

        //************************************Credit**************************************
        public async Task<List<int>> Credit(int userId, CancellationToken cancellationToken)
        {
            var Credit = await _context.Wallets
                     .Where(x => x.UserId == userId)
                     .Where(x => x.IsPay)
                     .Select(x => x.Amount)
                     .ToListAsync(cancellationToken);

            return Credit;
        }

        //************************************GetUserWallet*******************************
        public async Task<List<Wallet>> GetUserWallet(int userId, CancellationToken cancellationToken)
        {
            var userWalletList = await _context.Wallets
                    .Where(x => x.UserId == userId)
                    .Where(x => x.IsPay == true)
                    .Include(x => x.WalletTransactions)
                    .ToListAsync(cancellationToken);

            return userWalletList;
        }

        //************************************GetWalletByUserId***************************
        public async Task<Wallet> GetWalletByUserId(int userId, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets
                               .Where(x => x.UserId == userId)
                               .FirstOrDefaultAsync(cancellationToken);
            return wallet;
        }
    }
}
