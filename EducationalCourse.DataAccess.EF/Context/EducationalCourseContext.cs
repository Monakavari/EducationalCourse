using EducationalCourse.DataAccess.EF.Configurations;
using EducationalCourse.DataAccsess.EF.Configurations;
using EducationalCourse.Domain.Entities;
using EducationalCourse.Domain.Entities.Account;
using EducationalCourse.Domain.Entities.Order;
using EducationalCourse.Domain.Entities.Permission;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<UserDiscount> UserDiscounts { get; set; }
        public DbSet<CourseVote> CourseVotes { get; set; }


        #endregion DBSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseCommentConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseEpisodeConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseGroupConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseLevelConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseStatusConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDetailConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RolePermissionConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRoleConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PermissionConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDiscountConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserCourseConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WalletTransactionConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseVoteConfiguration).Assembly);
            
            //base.OnModelCreating(modelBuilder);
        }
    }
}
