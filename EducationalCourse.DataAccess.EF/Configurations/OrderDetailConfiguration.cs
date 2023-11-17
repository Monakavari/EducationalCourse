using EducationalCourse.Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder
               .HasOne(x => x.Order)
               .WithMany(x => x.OredrDetails)
               .HasForeignKey(x => x.OrderId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(x => x.Course)
               .WithMany(x => x.OrderDetails)
               .HasForeignKey(x => x.CourseId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
