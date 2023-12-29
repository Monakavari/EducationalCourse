using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Permission
{
    public class RolePermission :BaseEntity
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
