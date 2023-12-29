namespace EducationalCourse.Common.DTOs.Permission
{
    public class AssignRolePermissionDto
    {
        public AssignRolePermissionDto()
        {
            RoleIds = new List<int>();
            PermissionIds = new List<int>();
        }

        public List<int> RoleIds { get; set; }
        public List<int> PermissionIds { get; set; }

    }

}


