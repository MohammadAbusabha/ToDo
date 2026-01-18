using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDo.Core.Entities;
using ToDo.Core.Interfaces;
using ToDo.Core.Resources;

namespace ToDo.Core.Services
{
    public class JWTService : IJWTService
    {
        private readonly JwtSettingsResource _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public JWTService(IOptions<JwtSettingsResource> jwtSettings,
            UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtSettings.Value;
            _userManager = userManager;
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

            // user roles //

            foreach (var role in _userManager.GetRolesAsync(user).Result)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Token //

            var token = new JwtSecurityToken(issuer: "http://localhost:5298", audience: "MyApi", claims, expires: expire, signingCredentials: signCred);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}