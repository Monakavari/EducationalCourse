using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class CourseGroup : BaseEntity
    {
        public CourseGroup()
        {
            Children = new List<CourseGroup>();
            Courses = new List<Course>();
            SubCourseGroups = new List<Course>();
        }
        public string CourseGroupTitle { get; set; }
        public int? ParentId { get; set; }
        public CourseGroup Parent { get; set; }

        #region Relations

        public ICollection<CourseGroup> Children { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Course> SubCourseGroups { get; set; }

        #endregion
    }
}
