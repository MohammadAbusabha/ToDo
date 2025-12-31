using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Core.Entities;
using ToDo.Core.Resources;
using ToDo.Core.Interfaces;

namespace ToDo.Core.Services
{
    public class JWTService : IJWTService
    {
        private readonly JwtSettingsResource _jwtSettings;
        public JWTService(IOptions<JwtSettingsResource> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateJWTtoken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)); // Security key //
            var signCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Credentials //
            var expire = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.ExpirationMinutes)); // Token lifetime // 

            // user claims //

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // Token //

            var token = new JwtSecurityToken(issuer: "http://localhost:5298", audience: "MyApi", claims, expires: expire, signingCredentials: signCred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}