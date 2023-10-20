using EducationalCourse.Common.DTOs.Configurations;
using EducationalCourse.Domain.Models.Account;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EducationalCourse.Framework
{
    public static class JwtUtility
    {
        public static string GenerateJwtToken(User user, TokenSettings tokenSettings)
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
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(nameof(user.Email), user.Email),
                    new Claim(nameof(user.Mobile), user.Mobile),
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