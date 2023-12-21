using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Order;
using EducationalCourse.Common.Dtos.Wallet;
using EducationalCourse.Common.Enums;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Domain.Dtos.Wallet;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class WalletService : IWalletService
    {
        #region Constructor

        private readonly IWalletRepository _walletRepository;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUserCourseService _userCourseService;
        private static int _userId = 0;

        public WalletService(IWalletRepository walletRepository,
                             IUserRepository userRepository,
                             IHttpContextAccessor httpContextAccessor,
                             IUnitOfWork unitOfWork,
                             IOrderService orderService,
                             IUserCourseService userCourseService)
        {
            _walletRepository = walletRepository;
            _httpContextAccessor = httpContextAccessor;
            _UnitOfWork = unitOfWork;
            _userId = httpContextAccessor.GetUserId();
            _orderService = orderService;
            _userCourseService = userCourseService;
        }

        #endregion Constructor

        //*************************************** ChargeWallet ***************************************
        public async Task<ApiResult> ChargeWallet(ChargeWalletDto request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(_userId, cancellationToken);
            if (wallet is null)
                throw new AppException("برای کاربر جاری کیف پولی یافت نشد");

            wallet.UpdateDate = DateTime.Now;
            wallet.Amount += request.Amount;
            CreateWalletTransction(wallet, request);
            await _UnitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //*********************************************
        private void CreateWalletTransction(Wallet wallet, ChargeWalletDto request)
        {
            wallet.WalletTransactions.Add(new WalletTransaction
            {
                IsActive = true,
                Amount = request.Amount,
                Description = request.Description,
                WalletId = wallet.Id,
                WalletType = WalletTypeEnum.Credit
            });
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

        /// <summary>
        /// ساخت کیف پول و پرداخت و خرید دوره
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        #region RegisterAndFinalPayment

        //****************************** RegisterAndFinalPayment ****************************
        public async Task<ApiResult> RegisterAndFinalPayment(OrderForDepositDto request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(_userId, cancellationToken);
            var order = await GetOrder(request, cancellationToken);
            await NotExistWallet(wallet, order, cancellationToken);
            await ExistWallet(wallet, order, cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //***********************************************************
        private async Task<Order> GetOrder(OrderForDepositDto request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderById(request.OrderId, cancellationToken);
            if (order is null)
                throw new AppException("سفارشی یافت نشد.");

            return order;
        }

        //***********************************************************
        private async Task NotExistWallet(Wallet? wallet, Order order, CancellationToken cancellationToken)
        {
            if (wallet is null)
            {
                wallet = await CreateWallet(cancellationToken);
                CreditToWallet(wallet, order);
                DepositFromWallet(wallet, order);
                order.IsFinally = true;
                await _userCourseService.CreateUserCourse(order.OrderDetails.ToList(), _userId, cancellationToken);
                await _UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        //***********************************************************
        private async Task ExistWallet(Wallet? wallet, Order order, CancellationToken cancellationToken)
        {
            if (wallet is not null)
            {
                if (wallet.Amount < order.TotalPayment)
                    throw new AppException("موجودی کیف پول کافی نمیباشد.لطفا کیف پول خود را شارژ کنید.");

                DepositFromWallet(wallet, order);
                order.IsFinally = true;
                await _userCourseService.CreateUserCourse(order.OrderDetails.ToList(), _userId, cancellationToken);
                await _UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        //***********************************************************
        private async Task<Wallet> CreateWallet(CancellationToken cancellationToken)
        {
            var wallet = new Wallet
            {
                Amount = 0,
                IsActive = true,
                UserId = _userId,
            };
            await _walletRepository.AddAsync(wallet, cancellationToken);
            await _UnitOfWork.SaveChangesAsync(cancellationToken);

            return wallet;
        }

        //***********************************************************
        private void CreditToWallet(Wallet wallet, Order order)
        {
            //فرض میکنیم وارد درگاه شدیم و به اندازه پول سفارش ما در درگاه پرداختی انجام دادیم 
            //از درگاه با موقثیت برگشتیم 
            wallet.Amount = order.TotalPayment;
            wallet.UpdateDate = DateTime.Now;
            wallet.WalletTransactions.Add(new WalletTransaction
            {
                CreateDate = wallet.CreateDate,
                Amount = order.TotalPayment,
                Description = "واریز به حساب",
                IsActive = true,
                WalletId = wallet.Id,
                WalletType = WalletTypeEnum.Credit
            });
        }

        //***********************************************************
        private void DepositFromWallet(Wallet wallet, Order order)
        {
            wallet.Amount -= (int)order.TotalPayment;
            wallet.UpdateDate = DateTime.Now;
            wallet.WalletTransactions.Add(new WalletTransaction
            {
                CreateDate = wallet.CreateDate,
                Amount = (int)order.TotalPayment,
                Description = "برداشت از حساب",
                IsActive = true,
                WalletId = wallet.Id,
                WalletType = WalletTypeEnum.Deposit
            });
        }

        #endregion RegisterAndFinalPayment
    }
}
