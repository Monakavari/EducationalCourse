using EducationalCourse.Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccsess.EF.Configurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
        //    builder.HasKey(I => I.Id);
        //    builder.ToTable("Permissions");

            builder
               .Property(p => p.Title)
               .IsRequired(true)
               .HasMaxLength(100);

            builder
              .Property(p => p.Name)
              .IsRequired(true)
              .IsUnicode(true)
              .HasMaxLength(100);
        }
    }
}
