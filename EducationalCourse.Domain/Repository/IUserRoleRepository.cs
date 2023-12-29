using EducationalCourse.Domain.DTOs.Role;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<List<UserRole>> GetAllUserRolsAsync(CancellationToken cancellationToken);
    }
}
