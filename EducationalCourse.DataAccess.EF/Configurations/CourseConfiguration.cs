﻿using EducationalCourse.Domain.Models.Course;
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
                .HasForeignKey(x => x.CourseGroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.CourseStetus)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CourseStetusId);


            builder
                .HasOne(x => x.CourseLevel)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.CourseLevelId)
                .OnDelete(DeleteBehavior.Restrict);



            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Courses)
                .HasForeignKey(x => x.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
