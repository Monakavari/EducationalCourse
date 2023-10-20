using EducationalCourse.Common.Dtos.User;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Framework;

namespace EducationalCourse.ApplicationService.Services.Contracts
{
    public interface IUserService
    {
        #region LoginPage
        Task<ApiResult> SignUp(SignUpDto request, CancellationToken cancellationToken);
        Task<ApiResult<LoginResponseDto>> Login(LoginRequestDto request, CancellationToken cancellationToken);
        Task<ApiResult> ActiveUser(string activeCode, CancellationToken cancellationToken);
        Task<ApiResult<int>> ForgetPassword(string userName, CancellationToken cancellationToken);
        Task<ApiResult> RePassword(RePasswordRequestDto request, CancellationToken cancellationToken);

        #endregion LoginPage

        #region UserPannel
        Task<ApiResult<UserAccountInfoDto>> GetUserAccount(CancellationToken cancellationToken);
        Task<ApiResult> EditUserAccount(EditAccountUserDto request, CancellationToken cancellationToken);

        #endregion UserPannel
    }
}
