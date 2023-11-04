using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class CourseEpisode:BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public string EpisodeFileTitle { get; set; }
        public bool IsFree { get; set; }
    }
}
