namespace EducationalCourse.Common.DTOs.Permission
{
    public class EditPermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PermissionTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
