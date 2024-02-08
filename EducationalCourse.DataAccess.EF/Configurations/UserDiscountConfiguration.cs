using EducationalCourse.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    internal class UserDiscountConfiguration : IEntityTypeConfiguration<UserDiscount>
    {
        public void Configure(EntityTypeBuilder<UserDiscount> builder)
        {
            builder
              .HasOne(U => U.User)
              .WithMany(U => U.UserDiscounts)
              .HasForeignKey(U => U.UserId)
              .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(R => R.Discount)
                .WithMany(U => U.UserDiscounts)
                .HasForeignKey(R => R.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
