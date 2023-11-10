using Microsoft.AspNetCore.Http;

namespace EducationalCourse.Common.Dtos.Course
{
    public class EditCourseDto
    {
        public int Id { get; set; }
        public int CourseGroupId { get; set; }
        public int? SubCourseGroupId { get; set; }
        public int CourseLevelId { get; set; }
        public int CourseStatusId { get; set; }
        public int TeacherId { get; set; }
        public string CourseTitle { get; set; }
        public string DirectoryName { get; set; }
        public int CoursePrice { get; set; }
        public bool IsFreeCost { get; set; }
        public IFormFile FormFile { get; set; }
        public IFormFile DemoFile { get; set; }
    }
}
