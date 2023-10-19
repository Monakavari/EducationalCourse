using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Course
{
    public class CourseGroup : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string CourseGroupTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? ParentId { get; set; }
        public CourseGroup Parent { get; set; }

        #region Relations
        public ICollection<CourseGroup> Children { get; set; }
        public ICollection<Course> Courses { get; set; }

        #endregion
    }
}
