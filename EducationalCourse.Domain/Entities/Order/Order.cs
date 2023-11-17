using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OredrDetails = new List<OrderDetail>();
        }
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Discount { get; set; }
        public decimal TotalPayment { get; set; }
        public bool IsPaid { get; set; }

        public ICollection<OrderDetail> OredrDetails { get; set; }

    }
}
