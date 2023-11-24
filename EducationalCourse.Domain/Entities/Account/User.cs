using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Models.Account
{
    public class User : BaseEntity
    {
        public User()
        {
            CourseComments = new List<CourseComment>();
            Courses = new List<Course>();
            Orders = new List<Order>();
            UserCourses=new List<UserCourse>();
            Wallets = new List<Wallet>();

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string AvatarName { get; set; }
        public string AvatarBase64 { get; set; }
        public string ActiveCode { get; set; }
        public ICollection<CourseComment> CourseComments { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<Wallet> Wallets { get; set; }


    }
}
