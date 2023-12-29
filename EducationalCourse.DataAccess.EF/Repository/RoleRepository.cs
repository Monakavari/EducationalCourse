using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public RoleRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor

        //********************************** GetAllRoles ****************************
        public async Task<List<Role>> GetAllRoles(CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Where(x => x.IsActive)
                .Where(x => !x.IsDelete)
                .ToListAsync(cancellationToken);
        }

    }
}
