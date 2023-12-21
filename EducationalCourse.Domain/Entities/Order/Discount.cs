using EducationalCourse.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Entities.Order
{
    public class Discount:BaseEntity
    {
        public int MyProperty { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountPercent { get; set; }
        public int? UsableCount { get; set; } //تعداد قابل استفاده
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
