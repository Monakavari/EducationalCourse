using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;
using EducationalCourse.Domain.Models.Course;

namespace EducationalCourse.Domain.Entities
{
    public class CourseComment :BaseEntity
    {
        public CourseComment()
        {
            Children=new List<CourseComment>();
        }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public bool IsAdminRead { get; set; }
        public int? ParentId { get; set; }
        public CourseComment Parent { get; set; }
        public ICollection<CourseComment> Children { get; set; }
    }
}
