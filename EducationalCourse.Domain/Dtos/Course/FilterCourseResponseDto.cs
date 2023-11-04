namespace EducationalCourse.Domain.Dtos
{
    public class FilterCourseResponseDto
    {
        public string CourseTitle { get; set; }
        public string CourseImageName { get; set; }
        public string CourseImageBase64 { get; set; }
        public bool IsFreeCost { get; set; }
        public int CourseLevelId { get; set; }
        public int CourseStatusId { get; set; }
        public int TeacherId { get; set; }
        public long ViewCount { get; set; }

    }

}

