using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class Course : BaseEntity
    {
        public Course()
        {
            CourseComments = new List<CourseComment>();
            CourseEpisodes = new List<CourseEpisode>();
            OrderDetails = new List<OrderDetail>();
        }

        /// <summary>
        /// سرگروه
        /// </summary>
        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }

        /// <summary>
        /// زیرگروه
        /// </summary>
        public int? SubCourseGroupId { get; set; }
        public CourseGroup SubCourseGroup { get; set; }

        /// <summary>
        /// سطح دوره
        /// </summary>
        public int CourseLevelId { get; set; }
        public CourseLevel CourseLevel { get; set; }

        /// <summary>
        /// وضعیت دوره
        /// </summary>
        public int CourseStatusId { get; set; }
        public CourseStatus CourseStatus { get; set; }

        /// <summary>
        /// مدرس
        /// </summary>
        public int TeacherId { get; set; }
        public User User { get; set; }

        public string CourseTitle { get; set; }
        public string CourseImageName { get; set; }
        public string CourseImageBase64 { get; set; }
        public int CoursePrice { get; set; }
        public bool IsFreeCost { get; set; }
        public long ViewCount { get; set; }
        public string DemoVideo { get; set; }
        public ICollection<CourseComment> CourseComments { get; set; }
        public ICollection<CourseEpisode> CourseEpisodes { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }


    }
}
