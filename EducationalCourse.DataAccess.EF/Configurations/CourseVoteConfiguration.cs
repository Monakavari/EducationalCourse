using EducationalCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class CourseVoteConfiguration : IEntityTypeConfiguration<CourseVote>
    {
        public void Configure(EntityTypeBuilder<CourseVote> builder)
        {
            builder
                .HasOne(x => x.User)
                .WithMany(x => x.CourseVotes)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseVotes)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
