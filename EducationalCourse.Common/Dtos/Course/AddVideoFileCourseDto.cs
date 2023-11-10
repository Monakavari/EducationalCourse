using Microsoft.AspNetCore.Http;

namespace EducationalCourse.Common.Dtos.Course
{
    public class AddVideoFileCourseDto
    {
        public string CourseTitle { get; set; }
        public int CourseId { get; set; }
        public string EpisodeTime { get; set; }
        public string EpisodeFileTitle { get; set; }
        public bool IsFree { get; set; }
        public IFormFile VideoFiles { get; set; }
    }
}
