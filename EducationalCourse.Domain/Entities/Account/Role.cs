using EducationalCourse.Domain.Entities.Permission;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Account
{
    public class Role:BaseEntity
    {
        public Role()
        {
            RolePermissions =new List<RolePermission>();
            UserRoles = new List<UserRole>();
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
