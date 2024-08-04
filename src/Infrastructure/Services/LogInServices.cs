//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using SimpleAtm.Application.Common.Exceptions;
//using SimpleAtm.Application.Common.Interfaces;
//using SimpleAtm.Infrastructure.Identity;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace SimpleAtm.Infrastructure.Services;

//public class LogInServices
//{
//    public string GenerateJwtToken(string userName, ApplicationUser user, IConfiguration configuration)
//    {
//        var tokenHandler = new JwtSecurityTokenHandler();
//        var secretKey = configuration["JwtSettings:SecretKey"];
//        if (string.IsNullOrEmpty(secretKey))
//        {
//            throw new TokenConfigurationException("Secret key is missing.");
//        }
//        var key = Encoding.ASCII.GetBytes(secretKey);

//        var tokenDescriptor = new SecurityTokenDescriptor
//        {
//            Subject = new ClaimsIdentity(new Claim[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, userName.ToString()),
//            }),
//            Expires = DateTime.UtcNow.AddMinutes(20),
//            Issuer = configuration["JwtSettings:Issuer"],
//            Audience = configuration["JwtSettings:Audience"],
//            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//        };
//        var token = tokenHandler.CreateToken(tokenDescriptor);
//        return tokenHandler.WriteToken(token);
//    }
//}
