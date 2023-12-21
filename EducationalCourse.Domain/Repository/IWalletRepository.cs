using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IWalletRepository : IBaseRepository<Wallet>
    {
        Task<List<Wallet>> GetUserWallet(int userId, CancellationToken cancellationToken);
        Task<Wallet> GetWalletByUserId(int userId, CancellationToken cancellationToken);
    }
}
