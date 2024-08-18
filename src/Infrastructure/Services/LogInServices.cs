using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpleAtm.Application.Common.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleAtm.Infrastructure.Services;

public class LogInServices
{
    private readonly IConfiguration _configuration = null!;
    public LogInServices(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateJwtToken(string userName, Guid userId)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var audience = jwtSettings["Audience"];
        var issuer = jwtSettings["Issuer"];
        var secretKey = jwtSettings["SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
        {
            throw new TokenConfigurationException("Secret key is missing.");
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(1),
            Subject = GenerateClaims(userName, userId),
            Audience = audience,
            Issuer = issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private static ClaimsIdentity GenerateClaims(string userName, Guid userId)
    {
        var ci = new ClaimsIdentity();

        ci.AddClaim(new Claim("id", userId.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, userName));

        return ci;
    }
}
