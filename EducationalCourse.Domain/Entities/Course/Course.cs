using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Course
{
    public class Course : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CourseTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AvatarName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int CoursePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFreeCost { get; set; }
    }
}
