using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Dtos.User;
using EducationalCourse.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EducationalCourse.WebApi.Controllers
{
    public class UserController : BaseController
    {
        #region Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion Constructor

        #region SignUpAndLoginUser

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.SignUp(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.Login(request, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ActiveUser(string activeCode, CancellationToken cancellationToken = default)
        {
            var result = await _userService.ActiveUser(activeCode, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> ForgetPassword(string userName, CancellationToken cancellationToken = default)
        {
            var result = await _userService.ForgetPassword(userName, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RePassword(RePasswordRequestDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.RePassword(request, cancellationToken);
            return Ok(result);
        }

        #endregion SignUpAndLoginUser

        #region UserPannel

        [HttpGet]
        public async Task<IActionResult> GetUserAccount(CancellationToken cancellationToken = default)
        {
            var result = await _userService.GetUserAccount(cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> EditUserAccount([FromForm] EditAccountUserDto request, CancellationToken cancellationToken = default)
        {
            var result = await _userService.EditUserAccount(request, cancellationToken);
            return Ok(result);
        }

        #endregion UserPannel



    }
}
