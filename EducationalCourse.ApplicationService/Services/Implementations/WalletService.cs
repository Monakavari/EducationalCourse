using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Order;
using EducationalCourse.Common.Dtos.Wallet;
using EducationalCourse.Common.Enums;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class WalletService : IWalletServicecs
    {
        #region Constructor

        private readonly IWalletRepository _walletRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _UnitOfWork;
        private static int _userId = 0;

        public WalletService(IWalletRepository walletRepository,
                             IUserRepository userRepository,
                             IHttpContextAccessor httpContextAccessor,
                             IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _httpContextAccessor = httpContextAccessor;
            _UnitOfWork = unitOfWork;
            _userId = httpContextAccessor.GetUserId();
        }

        #endregion Constructor

        //*************************************** ChargeWallet ***************************************
        public async Task<ApiResult> ChargeWallet(ChargeWalletDto request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(_userId, cancellationToken);
            if (wallet is null)
                throw new AppException("برای کاربر جاری کیف پولی یافت نشد");

            wallet.UpdateDate = DateTime.Now;
            wallet.Amount = request.Amount;
            wallet.WalletTransactions = CreateWalletTransction(wallet, request);

            await _UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        private List<WalletTransaction> CreateWalletTransction(Wallet wallet, ChargeWalletDto request)
        {
            return new List<WalletTransaction>
            {
                new WalletTransaction
                {
                    WalletId = wallet.Id,
                    CreditAmount = request.Amount,
                    IsActive = true,
                    Description = "شارژ حساب",
                    WalletType = WalletTypeEnum.Credit
                }
            };
        }

        //*************************************** BalanceUserWallet **********************************
        public async Task<ApiResult<int>> BalanceUserWallet(CancellationToken cancellationToken)
        {
            var deposit = await _walletRepository.Deposit(_userId, cancellationToken);
            var credit = await _walletRepository.Credit(_userId, cancellationToken);

            var result = (credit.Sum() - deposit.Sum());

            return new ApiResult<int>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت  انجام شد.");

        }

        //*************************************** GetUserWallet **************************************
        public async Task<ApiResult<List<GetUserWalletDto>>> GetUserWallet(CancellationToken cancellationToken)
        {
            var result = new List<GetUserWalletDto>();
            var userWalletList = await _walletRepository.GetUserWallet(_userId, cancellationToken);

            foreach (var item in userWalletList)
            {
                result.Add(new GetUserWalletDto
                {
                    Amount = item.Amount,
                    CreatDateDisplay = item.CreateDate.ToShamsi(),
                });
            }

            if (result == null)
                throw new Exception("تراکنشی یافت نشد.");

            return new ApiResult<List<GetUserWalletDto>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت  انجام شد.");
        }

        //*************************************** RegisterAndFinalPayment **************************************
        public async Task<ApiResult> RegisterAndFinalPayment(OrderForDepositDto request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(_userId, cancellationToken);

            if (wallet is null)
            {
                wallet = await CreateWallet(cancellationToken);
                await CreditToWallet(wallet, request.TotalPayment, cancellationToken);
                //ToDo deposit from wallet
                return new ApiResult(true, ApiResultStatusCode.Success);
            }
            else if (wallet.Amount < request.TotalPayment)
                throw new AppException("موجودی کیف پول کافی نمیباشد");

            await CreditToWallet(wallet, request.TotalPayment, cancellationToken);
            //ToDo deposit from wallet

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        private async Task<Wallet> CreateWallet(CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                IsPay = false,
                Amount = 0,
                IsActive = true,
                UserId = _userId,
            };
            await _walletRepository.AddAsync(wallet, cancellationToken);
            await _UnitOfWork.SaveChangesAsync(cancellationToken);

            return wallet;
        }

        private async Task CreditToWallet(Wallet wallet, int totalPayment, CancellationToken cancellationToken)
        {
            //فرض میکنیم وارد درگاه شدیم و به اندازه پول سفارش ما در درگاه پرداختی انجام دادیم 
            //از درگاه با موقثیت برگشتیم 
            wallet.Amount = totalPayment;
            wallet.UpdateDate = DateTime.Now;
            wallet.WalletTransactions.Add(new WalletTransaction
            {
                CreateDate = wallet.CreateDate,
                CreditAmount = totalPayment,
                DepositAmount = 0,
                Description = "",
                IsActive = true,
                WalletId = wallet.Id,
                WalletType = WalletTypeEnum.Credit
            });

            await _UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
