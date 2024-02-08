using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Account
{
    public class UserDiscount:BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
