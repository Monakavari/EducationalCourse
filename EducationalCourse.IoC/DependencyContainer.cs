using EducationalCourse.ApplicationService.Mapper;
using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.ApplicationService.Services.Implementations;
using EducationalCourse.Common.DTOs.Configurations;
using EducationalCourse.DataAccess.EF.Context;
using EducationalCourse.DataAccess.EF.Repositories.Base;
using EducationalCourse.DataAccess.EF.Repository;
using EducationalCourse.Domain.ICommandRepositories.Base;
using EducationalCourse.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.DataAccess.EF.Repositories.Base;

namespace EducationalCourse.IOC
{
    public class DependencyContainer
    {
        public DependencyContainer()
        {

        }

        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            #region Rejester SiteSettings

            services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));

            #endregion Rejester SiteSettings

            #region Configure Sql

            services.AddDbContext<EducationalCourseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationalConnectionString")));

            #endregion

            #region Rejester Base

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion Rejester Base

            #region Rejester Repository

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseGroupRepository, CourseGroupRepository>();
            services.AddScoped<ICourseEpisodeRepository, CourseEpisodeRepository>();
            services.AddScoped<ICourseCommentRepository, CourseCommentRepository>();
            services.AddScoped<ICourseLevelRepository, CourseLevelRepository>();
            services.AddScoped<ICourseStatusRepository, CourseStatusRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IUserCourseRepository, UserCourseRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();



            #endregion Rejester Repository

            #region Rejester Servises

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseServise, CourseService>();
            services.AddScoped<ICourseGroupService, CourseGroupService>();
            services.AddScoped<IFileManagerService, FileManagerService>();
            services.AddScoped<ICourseEpisodeService, CourseEpisodeService>();
            services.AddScoped<ICourseCommentService, CourseCommentService>();
            services.AddScoped<IPrepareForViewService, PrepareForViewService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IUserCourseService, UserCourseService>();
            services.AddScoped<IDiscountService, DiscountService>();


            #endregion Rejester Servises

            #region Rejester AutoMapper

            services.AddAutoMapper(typeof(MappingUserProfile));

            #endregion

        }
    }
}
