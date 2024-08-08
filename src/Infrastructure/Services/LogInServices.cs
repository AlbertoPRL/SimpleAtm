using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleAtm.Application.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleAtm.Infrastructure.Services;

public class LogInServices(IConfiguration _configuration)
{
    public string GenerateJwtToken(string userName, string userId)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new TokenConfigurationException("Secret key is missing.");
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(userName, userId)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private static ClaimsIdentity GenerateClaims(string userName, string userId)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", userId.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, userName));

        return ci;
    }
}
