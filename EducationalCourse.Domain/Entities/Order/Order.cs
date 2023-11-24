using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            
        }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal TotalPayment { get; set; }
        public bool IsFinally { get; set; }
        public DateTime? PaymentDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
