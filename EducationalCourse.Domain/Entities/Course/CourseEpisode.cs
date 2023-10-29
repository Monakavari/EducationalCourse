using EducationalCourse.Domain.Models.Base;
using EducationalCourse.Domain.Models.Course;

namespace EducationalCourse.Domain.Entities
{
    public class CourseEpisode:BaseEntity
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Title { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
    }
}
