using EducationalCourse.DataAccess.EF.Configurations;
using EducationalCourse.Domain.Models.Course;
using EducationalCourse.Domain.Models.User;
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

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseGroup> CourseGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }

        #endregion DBSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
