namespace EducationalCourse.Common.DTOs.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string PermissionTitle { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string CreateDateDisplay { get; set; }
    }
}
