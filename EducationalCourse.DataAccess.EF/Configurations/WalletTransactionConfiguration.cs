using EducationalCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class WalletTransactionConfiguration : IEntityTypeConfiguration<WalletTransaction>
    {
        public void Configure(EntityTypeBuilder<WalletTransaction> builder)
        {
            builder
               .Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

            builder
              .HasOne(x => x.Wallet)
              .WithMany(x => x.WalletTransactions)
              .HasForeignKey(x => x.WalletId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
