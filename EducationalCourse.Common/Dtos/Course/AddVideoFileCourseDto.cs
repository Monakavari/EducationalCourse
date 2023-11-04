using Microsoft.AspNetCore.Http;

namespace EducationalCourse.Common.Dtos.Course
{
    public class AddVideoFileCourseDto
    {
        public string DirectoryName { get; set; }
        public int CourseId { get; set; }
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileTitle { get; set; }
        public string EpisodeFileName { get; set; }
        public bool IsFree { get; set; }
        public IFormFile VideoFiles { get; set; }
    }
}
