using EducationalCourse.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    internal class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder
                  .Property(p => p.DiscountCode)
                  .IsRequired(true)
                  .HasMaxLength(100);
        }
    }
}
