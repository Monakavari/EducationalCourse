using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Order
{
    public class Discount : BaseEntity
    {
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; } //تعداد قابل استفاده
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
