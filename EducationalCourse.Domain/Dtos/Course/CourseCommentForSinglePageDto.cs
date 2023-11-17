namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseCommentForSinglePageDto
    {
        public string Text { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CourseId { get; set; }
      
      public List<CourseCommentForSinglePageDto> Children { get; set; }
    }
}
