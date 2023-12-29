using AutoMapper;
using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.ApplicationService.Validations;
using EducationalCourse.Common.Dtos.User;
using EducationalCourse.Common.DTOs.Configurations;
using EducationalCourse.Common.Enums;
using EducationalCourse.Common.Extensions;
using EducationalCourse.Common.Utilities.Generator;
using EducationalCourse.Common.Utilities.Security;
using EducationalCourse.Domain.Dtos;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Repository;
using EducationalCourse.Framework;
using EducationalCourse.Framework.CustomException;
using EducationalCourse.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EducationalCourse.ApplicationService.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Constructor

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SiteSettings _siteSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileManagerService _fileManagerService;

        public UserService(IUserRepository userRepository,
                           IMapper mapper,
                           IUnitOfWork unitOfWork,
                           IOptions<SiteSettings> options,
                           IHttpContextAccessor httpContextAccessor,
                           IFileManagerService fileManagerService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _siteSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _fileManagerService = fileManagerService;
        }

        #endregion Constructor

        #region SignUpAndLoginUser

        //******************************* SignUp ******************************
        public async Task<ApiResult> SignUp(SignUpDto request, CancellationToken cancellationToken)
        {
            AccountValidator.SetValidators(request);
            await BusinessRole(request, cancellationToken);

            var entity = _mapper.Map<SignUpDto, User>(request);
            await _userRepository.AddAsync(entity, cancellationToken);

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("ثبت نام کاربر در هنگام عملیات دیتابیس با مشکل مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        //******************************* Login *******************************
        public async Task<ApiResult<LoginResponseDto>> Login(LoginRequestDto request, CancellationToken cancellationToken)
        {
            AccountValidator.SetValidators(request);

            var user = await _userRepository.GetUserByEmailOrMobile(request, cancellationToken);
            var hashPassword = PasswordHelper.EncodePasswordMd5(request.Password);

            if (user == null)
                throw new AppException("کاربری با مشخصات داده شده یافت نشد.");

            if (user.Password != hashPassword)
                throw new AppException("رمز عبور صحیح نمیباشد.");

            if (!user.IsActive )
                throw new AppException("کاربر فعال نمیباشد.");

            var token = JwtUtility.GenerateJwtToken(user, _siteSettings.TokenSettings);
            var loginResponseDto = new LoginResponseDto(user, token);


            return new ApiResult<LoginResponseDto>(true, ApiResultStatusCode.Success, loginResponseDto, "عملیات با موفقیت انجام شد.");

        }

        //******************************* ActiveUser **************************
        public async Task<ApiResult> ActiveUser(string activeCode, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByActiveCode(activeCode, cancellationToken);

            if (user is null)
                throw new AppException("کاربری با مشخصات داده شده یافت نشد.");

            user.ActiveCode = NameGenerator.GenerateUniqCode();
            user.IsActive = true;
            user.UpdateDate = DateTime.Now;
            user.Log = $" Active user by active code on {DateTime.Now}";

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);
        }

        //******************************* ForgetPassword **********************
        public async Task<ApiResult<int>> ForgetPassword(string userName, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUserName(userName, cancellationToken);

            if (!user.IsActive)
                throw new AppException("کاربر فعال نمی باشد.");

            if (user is null)
                throw new AppException("کاربر یافت نشد.");

            return new ApiResult<int>(true, ApiResultStatusCode.Success, user.Id, "عملیات با موفقیت انجام شد.");

        }

        //******************************* RePassword **************************
        public async Task<ApiResult> RePassword(RePasswordRequestDto request, CancellationToken cancellationToken)
        {
            AccountValidator.SetValidators(request);

            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var hashPassword = PasswordHelper.EncodePasswordMd5(request.Password);

            user.Password = hashPassword;
            user.Log += $" || Repassword user on {DateTime.Now}";

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult(true, ApiResultStatusCode.Success);

        }

        #region Business Roles
        private async Task BusinessRole(SignUpDto request, CancellationToken cancellationToken)
        {
            var checkUserRepeat = await _userRepository.IsExistUser(request, cancellationToken);

            if (checkUserRepeat)
                throw new AppException("User is repeatitive");
        }

        #endregion Business Roles

        #endregion SignUpAndLoginUser

        #region UserPannel

        //******************************* GetUserAccount **********************
        public async Task<ApiResult<UserAccountInfoDto>> GetUserAccount(CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            var result = new UserAccountInfoDto
            {
                CreateDate = user.CreateDate,
                CreateDateDisplay = user.CreateDate.ToShamsi(),
                Email = user.Email,
                FirstName = user.FirstName,
                LasttName = user.LastName,
                Mobile = user.Mobile,
                AvatarBase64 = user.AvatarBase64,
                AvatarName = user.AvatarName
            };
            //var result = _mapper.Map<User, UserAccountInfoDto>(user);

            return new ApiResult<UserAccountInfoDto>(true, ApiResultStatusCode.Success, result, "عملیات با موفقیت انجام شد.");
        }

        //******************************* EditUserAccount *********************
        public async Task<ApiResult> EditUserAccount(EditAccountUserDto request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            var saveImageResult = _fileManagerService.SaveImage(FileTypeEnum.UserImage, request.FormFile, user.Email, user.AvatarName);

            user.FirstName = request.FirstName;
            user.LastName = request.LasttName;
            user.AvatarName = saveImageResult?.AvatarName;
            user.AvatarBase64 = saveImageResult?.AvatarBase64;

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken)) > default(int);

            if (!result)
                throw new AppException("عملیات ویرایش در دیتابیس با خطا مواجه شد");

            return new ApiResult(true, ApiResultStatusCode.Success, "عملیات با موفقیت انجام شد.");
        }

        #endregion UserPannel
    }
}
