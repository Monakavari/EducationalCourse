using Microsoft.AspNetCore.Builder;
using EducationalCourse.Framework.Infrastructure.Middelewares;

namespace EducationalCourse.Framework.Infrastructure.Extensions
{
    public static class AppExtension
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>(Array.Empty<object>());
        }

        public static IApplicationBuilder UseJWTHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddelware>();
        }
    }
}
