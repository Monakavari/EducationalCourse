using EducationalCourse.DataAccess.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //services.Configure<SiteSettings>(configuration.GetSection(nameof(SiteSettings)));

            #endregion Rejester SiteSettings

            #region Configure Sql

            services.AddDbContext<EducationalCourseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("EducationalConnectionString")));

            #endregion

            #region Rejester Base

            //services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion Rejester Base

            #region Rejester CommandRepositories

            //services.AddScoped<IFileFormationCommandRepository, FileFormationCommandRepository>();

            #endregion Rejester CommandRepositories

            #region Rejester Servises

            //services.AddScoped<IPatientReceptionConsoleService, PatientReceptionConsoleService>();

            #endregion Rejester Servises
        }
    }
}
