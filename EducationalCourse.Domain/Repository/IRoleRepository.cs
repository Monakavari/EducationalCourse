using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.ICommandRepositories.Base;

namespace EducationalCourse.Domain.Repository
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<List<Role>> GetAllRoles(CancellationToken cancellationToken);
    }
}
