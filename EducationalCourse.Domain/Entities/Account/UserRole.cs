using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Account
{
    public class UserRole:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
