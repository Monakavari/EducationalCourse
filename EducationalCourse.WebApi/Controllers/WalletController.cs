using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Order;
using EducationalCourse.Common.Dtos.Wallet;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class WalletController : BaseController
    {
        #region Constructor

        private readonly IWalletService _walletServicecs;

        public WalletController(IWalletService walletServicecs)
        {
            _walletServicecs = walletServicecs;
        }

        #endregion Constructor

        [HttpPost]
        public async Task<IActionResult> ChargeWallet(ChargeWalletDto request, CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.ChargeWallet(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWallet(CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.GetUserWallet(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAndFinalPayment(OrderForDepositDto request, CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.RegisterAndFinalPayment(request, cancellationToken);
            return Ok(result);
        }

    }
}
