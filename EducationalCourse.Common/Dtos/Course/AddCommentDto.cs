namespace EducationalCourse.Common.Dtos.Course
{
    public class AddCommentDto
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
