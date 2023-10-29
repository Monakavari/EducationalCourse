using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Course
{
    public class Course : BaseEntity
    {
        public Course()
        {
            CourseComments=new List<CourseComment>();
            CourseEpisodes=new List<CourseEpisode>();
        }

        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public int CourseLevelId { get; set; }
        public CourseLevel CourseLevel { get; set; }
        public int CourseStetusId { get; set; }
        public CourseStatus CourseStetus { get; set; }
        public int TeacherId { get; set; }
        public User User { get; set; }
        public string CourseTitle { get; set; }
        public string CourseImageName { get; set; }
        public string CourseImageBase64 { get; set; }
        public int CoursePrice { get; set; }
        public bool IsFreeCost { get; set; }
        public long ViewCount { get; set; }
        public ICollection<CourseComment> CourseComments { get; set; }
        public ICollection<CourseEpisode> CourseEpisodes { get; set; }

    }
}
