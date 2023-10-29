using EducationalCourse.Domain.Models.Base;
using EducationalCourse.Domain.Models.Course;

namespace EducationalCourse.Domain.Entities
{
    public class CourseStatus:BaseEntity
    {
        public CourseStatus()
        {
            Courses=new List<Course>();
        }
        public string Title { get; set; }
        public ICollection<Course> Courses { get; set; }
       
    }
}
