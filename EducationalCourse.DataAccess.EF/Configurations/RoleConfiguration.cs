using EducationalCourse.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccsess.EF.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            //builder.HasKey(I => I.Id);
            //builder.ToTable("Roles");

            builder
                  .Property(p => p.Title)
                  .IsRequired(true)
                  .HasMaxLength(15);

            builder
                  .Property(p => p.Name)
                  .IsRequired(true)
                  .IsUnicode(true)
                  .HasMaxLength(20);
        }
    }
}
