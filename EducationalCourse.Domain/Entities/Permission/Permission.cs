using EducationalCourse.Common.Enums;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Permission
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public PermissionCodeEnum PermissionCode { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set;}
    }
}
