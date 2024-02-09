using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities
{
    public class CourseVote : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public bool Vote { get; set; }
    }
}
