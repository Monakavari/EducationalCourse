using EducationalCourse.Common.DTOs.Configurations;
using EducationalCourse.Domain.DTOs.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sample.Framework
{
    public static class JwtUtility
    {
        public static string GenerateJwtToken(GetUserDataForGenerateTokenDto user, TokenSettings tokenSettings)
        {
            TimeSpan TokenLifetime = TimeSpan.FromHours(tokenSettings.TokenExpiresInHours);
            byte[] key = Encoding.ASCII.GetBytes(tokenSettings.SecretKey);
            
            var symmetricSecurityKey = new SymmetricSecurityKey(key);

            var securityAlgoritm = SecurityAlgorithms.HmacSha256Signature;

            var signingCredential = new SigningCredentials(symmetricSecurityKey, securityAlgoritm);

            var tokenDiscriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (new[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim(nameof(user.Email), user.Email),
                    new Claim(nameof(user.FullName), user.FullName),
                    new Claim(nameof(user.Mobile), user.Mobile),
                    new Claim("RoleId", string.Join("," , user.RoleIds)),
                    new Claim("PermissionId", string.Join("," , user.PermisionIds))
                }),

                Expires = DateTime.UtcNow.Add(TokenLifetime),
                SigningCredentials = signingCredential,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDiscriptor);

            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}