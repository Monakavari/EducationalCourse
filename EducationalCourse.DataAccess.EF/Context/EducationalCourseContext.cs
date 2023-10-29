using EducationalCourse.DataAccess.EF.Configurations;
using EducationalCourse.Domain.Models.Account;
using EducationalCourse.Domain.Models.Course;
using Microsoft.EntityFrameworkCore;

namespace EducationalCourse.DataAccess.EF.Context
{
    public class EducationalCourseContext : DbContext
    {
        public EducationalCourseContext()
        {
        }

        public EducationalCourseContext(DbContextOptions<EducationalCourseContext> options) : base(options)
        {
        }

        #region DBSet
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseGroup> CourseGroups { get; set; }
        public DbSet<CourseGroup> CourseComments { get; set; }
        public DbSet<CourseGroup> CourseEpisodes { get; set; }
        public DbSet<CourseGroup> CourseStatuses { get; set; }
        public DbSet<CourseGroup> CourseLeveles { get; set; }
       
        #endregion DBSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
