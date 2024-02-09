namespace EducationalCourse.Common.Dtos.Course
{
    public class AddRepliedCommentDto
    {
        public string Text { get; set; }
        public int CourseId { get; set; }
        public int CommentId { get; set; }
    }
}
