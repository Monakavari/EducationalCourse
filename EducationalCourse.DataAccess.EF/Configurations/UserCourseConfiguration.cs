using EducationalCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder
               .HasOne(x => x.User)
               .WithMany(x => x.UserCourses)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(x => x.Course)
              .WithMany(x => x.UserCourses)
              .HasForeignKey(x => x.CourseId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
