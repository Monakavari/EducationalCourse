namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseEpisodeDto
    {
        public int Id { get; set; }
        public string EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public string EpisodeFileTitle { get; set; }
        public bool IsFree { get; set; }
    }
}
