using EducationalCourse.DataAccess.EF.Configurations;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Models.Account;
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
        public DbSet<CourseComment> CourseComments { get; set; }
        public DbSet<CourseEpisode> CourseEpisodes { get; set; }
        public DbSet<CourseStatus> CourseStatuses { get; set; }
        public DbSet<CourseLevel> CourseLeveles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Discount> Discounts { get; set; }
       
              
        #endregion DBSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);

            //base.OnModelCreating(modelBuilder);
        }
    }
}
