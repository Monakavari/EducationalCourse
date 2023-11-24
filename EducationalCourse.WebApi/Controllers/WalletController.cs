using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Wallet;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class WalletController : BaseController
    {
        #region Constructor

        private readonly IWalletServicecs _walletServicecs;

        public WalletController(IWalletServicecs walletServicecs)
        {
            _walletServicecs = walletServicecs;
        }

        #endregion Constructor

        //******************************ChargeWallet**********************************

        [HttpPost]
        public async Task<IActionResult> ChargeWallet(ChargeWalletDto request, CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.ChargeWallet(request, cancellationToken);
            return Ok(result);
        }

        //******************************BalanceUserWallet*****************************

        [HttpGet]
        public async Task<IActionResult> BalanceUserWallet(CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.BalanceUserWallet(cancellationToken);
            return Ok(result);
        }

        //******************************GetUserWallet*********************************

        [HttpGet]
        public async Task<IActionResult> GetUserWallet(CancellationToken cancellationToken = default)
        {
            var result = await _walletServicecs.GetUserWallet(cancellationToken);
            return Ok(result);
        }
    }
}
