using EducationalCourse.ApplicationService.Services.Contracts;
using EducationalCourse.Common.Enums;
using EducationalCourse.Framework.CustomException;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationalCourse.WebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermissionCheckerAttribute : Attribute, IAsyncActionFilter
    {
        private readonly PermissionCodeEnum _permissionCode;
        public PermissionCheckerAttribute(PermissionCodeEnum permissionCode)
        {
            _permissionCode = permissionCode;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var scope = context.HttpContext.RequestServices.CreateScope())
            {
                var httpContextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
                //when role and permission are in token
                // var ss =  httpContextAccessor.GetPermisionIds(); 
                var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

                await CheckPermission(permissionService, httpContextAccessor);
            }

            await next();
        }

        private async Task CheckPermission(IPermissionService permissionService, IHttpContextAccessor httpContextAccessor)
        {
           // var permissionId = await permissionService.GetPermissionIdByCode(_permissionCode, default);
            var grantPermission = await permissionService.GrantPermission(_permissionCode, default);

            if (!grantPermission)
                throw new AppException("Unauthorized");
            //when role and permission are in token
            //var permissionIds = httpContextAccessor.GetPermisionIds();
            //if (!permissionIds.Any(c => c == permissionId))
            //    throw new AppException("Unauthorized");
        }
    }
}
