namespace EducationalCourse.Domain.Dtos.Course
{
    public class FilterCourseDto
    {
        public string CourseTitle { get; set; }
        public bool IsFreeCost { get; set; }
        public int CoursePrice { get; set; }
        public string CourseImageBase64 { get; set; }
        public string CourseImageName { get; set; }
        public string CreatDateDisplay { get; set; }

    }
}
