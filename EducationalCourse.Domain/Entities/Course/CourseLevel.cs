using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class CourseLevel : BaseEntity
    {
        public CourseLevel()
        {
            Courses = new List<Course>();
        }
        public string Title { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
