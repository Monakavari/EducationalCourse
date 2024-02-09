namespace EducationalCourse.Common.Dtos.Course
{
    public class AddUserVoteDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool Vote { get; set; }
    }
}
