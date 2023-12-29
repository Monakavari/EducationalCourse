using EducationalCourse.Common.DTOs.Role;

namespace EducationalCourse.Common.Dtos.Role
{
    public class AssignUserRoleDto
    {
        public AssignUserRoleDto()
        {
            UserRoles = new List<UserRoleDto>();
        }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
