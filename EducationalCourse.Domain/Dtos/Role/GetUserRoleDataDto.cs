﻿namespace EducationalCourse.Domain.DTOs.Role
{
    public class GetUserRoleDataDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string FullName { get; set; }
    }
}
