using Accounts.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Accounts.Services
{
  public class TokenService
  {
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public TokenService(UserManager<User> userManager, IConfiguration config)
    {
      _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public async Task<string> GenerateToken(User user)
    {
      var claims = new List<Claim>
      {
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.UserName),
      };

      var roles = await _userManager.GetRolesAsync(user);
      foreach(var role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));

      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

      var tokenOptions = new JwtSecurityToken(
        issuer: _config["JWTSettings:ValidIssuer"],
        audience: _config["JWTSettings:ValidAudience"],
        claims: claims,
        expires: DateTime.UtcNow.AddDays(int.Parse(_config["JWTSettings:TokenExpirationDays"])),
        signingCredentials: creds
        );

      return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
  }
}
