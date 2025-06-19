using Hotel.AccountManagement.Entities;
using Hotel.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccountManagement.Services
{
    public class TokenService(UserManager<User> userManager) : ITokenService
    {
        public async Task <string> CreateToken(User user)
        {
            var tokenKey = "best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token--best-secure-token";
            if (tokenKey.Length < 64) throw new Exception("Token key has to be longer! Try again!");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            if (user.UserName == null) throw new Exception("No user with this username");
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Name,user.UserName)
            };
            var roles = await userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
