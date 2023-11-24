using EducationalCourse.Common.Dtos.Order;
using EducationalCourse.Common.Dtos.Wallet;
using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IWalletServicecs
    {
        Task<ApiResult> ChargeWallet(ChargeWalletDto request, CancellationToken cancellationToken);
        Task<ApiResult> RegisterAndFinalPayment(OrderForDepositDto request, CancellationToken cancellationToken);
        Task<ApiResult<int>> BalanceUserWallet(CancellationToken cancellationToken);
        Task<ApiResult<List<GetUserWalletDto>>> GetUserWallet(CancellationToken cancellationToken);


    }
}
