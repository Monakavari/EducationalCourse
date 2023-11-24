using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.Domain.Dtos.Order
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public int CourseId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
