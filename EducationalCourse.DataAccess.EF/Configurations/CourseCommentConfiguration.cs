using EducationalCourse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalCourse.DataAccess.EF.Configurations
{
    public class CourseCommentConfiguration : IEntityTypeConfiguration<CourseComment>
    {
        public void Configure(EntityTypeBuilder<CourseComment> builder)
        {
            builder
                .Property(x => x.Text)
                .IsRequired(true)
                .HasMaxLength(100);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.CourseComments)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
              .HasOne(x => x.User)
              .WithMany(x => x.CourseComments)
              .HasForeignKey(x => x.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.ParentId)
                .IsRequired(false);

            builder
                .HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
