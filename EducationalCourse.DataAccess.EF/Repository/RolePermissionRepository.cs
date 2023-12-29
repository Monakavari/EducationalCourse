using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.Domain.Entities.Permission;
using EducationalCourse.Domain.Repository;

namespace EducationalCourse.DataAccess.EF.Repository
{
    public class RolePermissionRepository : BaseRepository<RolePermission>, IRolePermissionRepository
    {
        #region Constructor

        private readonly EducationalCourseContext _context;

        public RolePermissionRepository(EducationalCourseContext context) : base(context)
        {
            _context = context;
        }

        #endregion Constructor
    }
}
