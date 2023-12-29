using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.DTOs.Role;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public UserRoleRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //************************ GetAllUserRolsAsync *************************
        public async Task<List<UserRole>> GetAllUserRolsAsync(CancellationToken cancellationToken)
        {
            return await _context.UserRoles
                .Where(x => x.IsActive)
                .Where(x => !x.IsDelete)
                .ToListAsync(cancellationToken);
        }
    }
}
