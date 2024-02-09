using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.Discount;
using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class DiscountService : IDiscountService
    {
        #region Constructor

        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserDiscountRepository _userDiscountRepository;
        // private readonly IHttpContextAccessor _httpContextAccessor;
        private static int _userId = 0;
        public DiscountService(
                         IUnitOfWork unitOfWork,
                         IDiscountRepository discountRepository,
                         IOrderRepository orderRepository,
                         IUserDiscountRepository userDiscountRepository,
                         IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _discountRepository = discountRepository;
            _orderRepository = orderRepository;
            _userDiscountRepository = userDiscountRepository;
            //  _httpContextAccessor = httpContextAccessor;
            _userId = httpContextAccessor.GetUserId();
        }

        #endregion Constructor

        //********************************* UseDiscount *****************************
        public async Task<ApiResult> UseDiscount(UseDiscountDto request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.GetDiscountByCode(request.DiscountCode, cancellationToken);
            await CheckDiscountUseType(discount, cancellationToken);
            await AssignDiscountToOrder(request, discount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //***************************** CreateDiscount ******************************
        public async Task<ApiResult> CreateDiscount(CreateDiscountDto request, CancellationToken cancellationToken)
        {
            var discount = new Discount()
            {
                DiscountCode = request.DiscountCode,
                DiscountPercent = request.DiscountPercent,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UsableCount = request.UsableCount,
            };

            if (await _discountRepository.IsExistDiscountCode(request.DiscountCode, cancellationToken))
                throw new AppException("کد تخفیف تکراری میباشد.");

            await _discountRepository.AddAsync(discount, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //********************************* GetAllDiscounts *************************
        public async Task<ApiResult<List<Discount>>> GetAllDiscounts(CancellationToken cancellationToken)
        {
            var result = await _discountRepository.GetAllDiscounts(cancellationToken);

            return new ApiResult<List<Discount>>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");

        }

        //********************************* UpdateDiscount **************************
        public async Task<ApiResult> UpdateDiscount(UpdateDiscountDto request, CancellationToken cancellationToken)
        {
            var discount = await _discountRepository.GetDiscountById(request.Id, cancellationToken);

            discount.DiscountCode = request.DiscountCode;
            discount.DiscountPercent = request.DiscountPercent;
            discount.StartDate = request.StartDate;
            discount.EndDate = request.EndDate;
            discount.UsableCount = request.UsableCount;

            _discountRepository.Update(discount);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        #region UseDiscount-Utilities

        private async Task AssignDiscountToOrder(UseDiscountDto request, Discount discount, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            int persent = order.TotalPayment * discount.DiscountPercent / 100;
            order.TotalPayment = order.TotalPayment - persent;
            _orderRepository.Update(order);
            DecreseUseableCount(discount);
            await AddUserDiscountUsed(discount, order, cancellationToken);
        }
        private void DecreseUseableCount(Discount discount)
        {
            if (discount.UsableCount != null)
            {
                discount.UsableCount -= 1;
            }
            _discountRepository.Update(discount);
        }
        private async Task AddUserDiscountUsed(Discount discount, Order order, CancellationToken cancellationToken)
        {
            var userDiscount = new UserDiscount()
            {
                UserId = order.UserId,
                DiscountId = discount.Id
            };
            await _userDiscountRepository.AddAsync(userDiscount, cancellationToken);
        }
        private async Task<DiscountUseType> CheckDiscountUseType(Discount discount, CancellationToken cancellationToken)
        {
            if (discount == null)
                return DiscountUseType.NotFound;

            if (discount.StartDate != null && discount.StartDate > DateTime.Now)
                return DiscountUseType.NotStarted;

            if (discount.EndDate != null && discount.EndDate < DateTime.Now)
                return DiscountUseType.Expired;

            if (discount.UsableCount != null && discount.UsableCount < 1)
                return DiscountUseType.Finished;

            if (await _userDiscountRepository.CheckUserDiscount(_userId, discount.Id, cancellationToken))
                return DiscountUseType.Used;

            return DiscountUseType.Success;
        }

        #endregion UseDiscount-Utilities
    }
}
