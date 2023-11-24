namespace EducationalCourse.Common.Dtos.Course
{
    public class AddOrderDetailDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Count { get; set; }
    }
}
