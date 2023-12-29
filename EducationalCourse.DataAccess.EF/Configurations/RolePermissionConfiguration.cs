using EducationalCourse.Domain.Entities.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccsess.EF.Configurations
{
    internal class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            //builder.HasKey(I => I.Id);
            //builder.ToTable("RolePermissions");

            builder
                .HasOne(R => R.Role)
                .WithMany(RP => RP.RolePermissions)
                .HasForeignKey(P => P.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(P => P.Permission)
                .WithMany(RP => RP.RolePermissions)
                .HasForeignKey(R => R.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
