using EducationalCourse.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(I => I.Id);
            builder.ToTable("Courses");

            builder
                .Property(x => x.CourseTitle)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .Property(x => x.CourseImageName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
               .Property(x => x.CourseImageBase64)
               .IsRequired(true)
               .HasMaxLength(100); 

            builder
                .HasOne(x => x.CourseGroup)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CourseGroupId);
        }
    }
}
