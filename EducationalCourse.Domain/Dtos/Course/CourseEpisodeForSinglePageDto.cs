namespace EducationalCourse.Domain.Dtos.Course
{
    public class CourseEpisodeForSinglePageDto
    {
        public string EpisodeTime { get; set; }
        public TimeSpan EpisodeTimeSpan { get; set; }
        public TimeSpan TotalEpisodeTime { get; set; }
        public string EpisodeFileTitle { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
    }
}
