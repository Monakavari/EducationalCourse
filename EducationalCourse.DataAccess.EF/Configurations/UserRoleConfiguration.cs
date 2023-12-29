using EducationalCourse.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccsess.EF.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //builder.HasKey(I => I.Id);
            //builder.ToTable("UserRole");

            builder
                .HasOne(U => U.User)
                .WithMany(U => U.UserRoles)
                .HasForeignKey(U => U.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(R => R.Role)
                .WithMany(U => U.UserRoles)
                .HasForeignKey(R => R.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
