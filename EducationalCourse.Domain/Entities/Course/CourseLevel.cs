using EducationalCourse.Domain.Models.Base;
using EducationalCourse.Domain.Models.Course;

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
