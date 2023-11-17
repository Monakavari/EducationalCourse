using EducationalCourse.Domain.Dtos.Course;

namespace EducationalCourse.Domain.Dtos
{
    public class CourseSinglePageDto
    {
        public CourseSinglePageDto()
        {
            Episodes = new List<CourseEpisodeForSinglePageDto>();
            Comments = new List<CourseCommentForSinglePageDto>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public int LevelId { get; set; }
        public string LevelTitle { get; set; }

        public int StatusId { get; set; }
        public string StatusTitle { get; set; }

        public int CoursePrice { get; set; }
        public int CourseId { get; set; }

        public int CourseGroupId { get; set; }
        public string CourseGroupTitle { get; set; }
        public int? SubCourseGroupId { get; set; }
        public string SubCourseGroupTitle { get; set; }
        public int TotalEpisodeFileCount { get; set; }
        public TimeSpan TotalEpisodeTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string ShowCreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ShowUpdateDate { get; set; }

        public List<CourseEpisodeForSinglePageDto> Episodes { get; set; }
        public List<CourseCommentForSinglePageDto> Comments { get; set; }
    }
}
