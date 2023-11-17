using EducationalCourse.Domain.Models.Base;

namespace EducationalCourse.Domain.Entities.Order
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }

    }
}
