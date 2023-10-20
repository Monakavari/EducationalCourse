using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Course
{
    public class Course : BaseEntity
    {

        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public string CourseTitle { get; set; }
        public string CourseImageName { get; set; }
        public string CourseImageBase64 { get; set; }
        public int CoursePrice { get; set; }
        public bool IsFreeCost { get; set; }
        public long ViewCount { get; set; }
    }
}
