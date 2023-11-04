namespace EducationalCourse.Common.Dtos
{
    public class FilterCourseRequestDto : BasePagingDto
    {
        public int? CourseGroupId { get; set; }
        public int? SubCourseGroupId { get; set; }
        public string CourseTitle { get; set; }
        public int? StartPrice { get; set; }
        public int? EndPrice { get; set; }
        public bool IsFreeCost { get; set; }

    }
}
