using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Course
{
    public class CourseGroup : BaseEntity
    {
        public CourseGroup()
        {
            Children =new List<CourseGroup>();
            Courses = new List<Course>();
        }
        public string CourseGroupTitle { get; set; }
        public string Lineage { get; set; }
        public int DirectChildCount { get; set; }
        public int ParentCount { get; set; }
        public int? ParentId { get; set; }
        public CourseGroup Parent { get; set; }

        #region Relations
        public ICollection<CourseGroup> Children { get; set; }
        public ICollection<Course> Courses { get; set; }

        #endregion
    }
}
