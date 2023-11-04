namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseGroupDto
    {
        public int id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public List<CourseGroupDto> Children { get; set; }
    }
}
