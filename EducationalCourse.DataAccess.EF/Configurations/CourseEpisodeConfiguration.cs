using EducationalCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class CourseEpisodeConfiguration : IEntityTypeConfiguration<CourseEpisode>
    {
        public void Configure(EntityTypeBuilder<CourseEpisode> builder)
        {
            builder
                .Property(x => x.EpisodeFileTitle)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
               .Property(x => x.EpisodeFileName)
               .IsRequired(true)
               .HasMaxLength(100);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseEpisodes)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
