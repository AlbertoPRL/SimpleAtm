using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Application.Common.Models;
using SimpleAtm.Infrastructure.Services;

namespace SimpleAtm.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    //private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IConfiguration _configuration;
    private readonly LogInServices _logInServices;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        //SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IConfiguration configuration,
        LogInServices logInServices)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        //_signInManager = signInManager;
        _configuration = configuration;
        _logInServices = logInServices;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = userName,
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<ApplicationSignInResult> SignInAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return ApplicationSignInResult.Failure(new string[] { "User does not exist.", });
        }
        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
        {
            return ApplicationSignInResult.Failure(new string[] { "Invalid password/Email.", });
        }
       //var canSignIn = _signInManager.CanSignInAsync(user);
        var token = _logInServices.GenerateJwtToken(userName, user.Id);
        return new ApplicationSignInResult(true, token);
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
        
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    //public string GenerateJwtToken(string userName, string userId)
    //{
    //    var jwtSettings = _configuration.GetSection("JwtSettings");
    //    var secretKey = jwtSettings["SecretKey"];
    //    if (string.IsNullOrEmpty(secretKey))
    //    {
    //        throw new TokenConfigurationException("Secret key is missing.");
    //    }
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(secretKey);

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new Claim[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
    //            new Claim(ClaimTypes.Name, userName.ToString()),
    //        }),
    //        Expires = DateTime.UtcNow.AddMinutes(20),
    //        Issuer = _configuration["JwtSettings:Issuer"],
    //        Audience = _configuration["JwtSettings:Audience"],
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return tokenHandler.WriteToken(token);
    //}
    //public string GenerateJwtToken(string userName, string userId)
    //{
    //    var jwtSettings = _configuration.GetSection("JwtSettings");
    //    var secretKey = jwtSettings["SecretKey"];
    //    if (string.IsNullOrEmpty(secretKey))
    //    {
    //        throw new TokenConfigurationException("Secret key is missing.");
    //    }
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(secretKey);

    //    var credentials = new SigningCredentials(
    //        new SymmetricSecurityKey(key),
    //        SecurityAlgorithms.HmacSha256);

    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        SigningCredentials = credentials,
    //        Expires = DateTime.UtcNow.AddHours(1),
    //        Subject = GenerateClaims(userName, userId)
    //    };

    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return tokenHandler.WriteToken(token);
    //}
    //private static ClaimsIdentity GenerateClaims(string userName, string userId)
    //{
    //    var ci = new ClaimsIdentity();

    //    ci.AddClaim(new Claim("id", userId.ToString()));
    //    ci.AddClaim(new Claim(ClaimTypes.Name, userName));

    //    return ci;
    //}
}
