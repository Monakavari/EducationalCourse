using EducationalCourse.Common.Dtos.User;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Models.Account;

namespace EducationalCourse.Domain.Repository
{
    public interface IUserRepository :IBaseRepository<User>
    {
        Task<bool> IsExistUser(SignUpDto request, CancellationToken cancellationToken);
        Task<User> GetUserByEmailOrMobile(LoginRequestDto request, CancellationToken cancellationToken);
        Task<User> GetUserByActiveCode(string activeCode, CancellationToken cancellationToken);
        Task<User> GetUserByUserName(string userName, CancellationToken cancellationToken);
    }
}
