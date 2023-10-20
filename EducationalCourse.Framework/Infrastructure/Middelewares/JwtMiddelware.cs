using EducationalCourse.Common.DTOs.Configurations;
using EducationalCourse.Domain.Models.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace EducationalCourse.Framework.Infrastructure.Middelewares
{
    public class JwtMiddelware
    {
        protected RequestDelegate Next { get; }
        protected IConfiguration Configuration { get; }

        public JwtMiddelware(RequestDelegate next, IConfiguration configuration) : base()
        {
            Next = next;
            Configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            var siteSettings = Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
            var requestheaders = context.Request.Headers["Authorization"];

            string token = requestheaders
                    .FirstOrDefault()?
                    .Split(" ")
                    .Last();

            if (token is not null && string.IsNullOrWhiteSpace(token) == false)
            {
                AttachUsertoContextByToken(context, token, siteSettings.TokenSettings.SecretKey);
            }

            await Next(context);
        }

        private void AttachUsertoContextByToken(HttpContext context, string token, string secretKey)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(secretKey);

                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = System.TimeSpan.Zero,
                }, out SecurityToken validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return;
                }

                var userId = jwtToken.Claims.FirstOrDefault(current => current.Type.Equals("UserId"))?.Value;
                if (userId is null)
                    return;

                var user = new User
                {
                    Id = int.Parse(userId),
                    Email = jwtToken.Claims.FirstOrDefault(current => current.Type.Equals("Email"))?.Value,
                    Mobile = jwtToken.Claims.FirstOrDefault(current => current.Type.Equals("Mobile"))?.Value,
                };

                context.Items["User"] = user;
                //context.Items["RoleId"] = jwtToken.Claims.FirstOrDefault(current => current.Type.Equals("RoleId"))?.Value;
                //context.Items["PermissionId"] = jwtToken.Claims.FirstOrDefault(current => current.Type.Equals("PermissionId"))?.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}