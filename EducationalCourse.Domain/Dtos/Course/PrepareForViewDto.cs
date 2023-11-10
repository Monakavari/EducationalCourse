namespace EducationalCourse.Domain.Dtos.Course
{
    public class PrepareForViewDto
    {
        public PrepareForViewDto()
        {
            CourseGroup = new List<CourseGroupDto>();
            CourseStatus = new List<CourseStatusDto>();
            CourseLevel = new List<CourseLevelDto>();
            User = new List<UserDto>();
        }
        public List<CourseGroupDto> CourseGroup { get; set; }
        public List<CourseStatusDto> CourseStatus { get; set; }
        public List<CourseLevelDto> CourseLevel { get; set; }
        public List<UserDto> User { get; set; }

    }
}
