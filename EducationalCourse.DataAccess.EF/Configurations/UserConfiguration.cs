using EducationalCourse.Domain.Models.User;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.HasKey(I => I.Id);
            builder.ToTable("Users");

            builder
                .Property(F => F.FirstName)
                .HasMaxLength(50);

            builder
                .Property(L => L.LastName)
                .HasMaxLength(50);

            builder
                .Property(E => E.Email)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.UserName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.AvatarName)
                .HasMaxLength(100);
            
            builder
                .Property(x => x.ActiveCode)
                .HasMaxLength(100);

            builder
               .Property(P => P.Password)
               .IsRequired(true)
               .HasMaxLength(250);

            builder
               .Property(M => M.Mobile)
               .IsRequired(true)
               .HasMaxLength(11);
        }
    }
}
