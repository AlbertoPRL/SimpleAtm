using System.Net.Http;
using SimpleAtm.Application.Common.Models;

namespace SimpleAtm.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(Guid userId);

    Task<bool> IsInRoleAsync(Guid userId, string role);

    Task<bool> AuthorizeAsync(Guid userId, string policyName);

    Task<(Result Result, Guid UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(Guid userId);

    Task<ApplicationSignInResult> SignInAsync(string userName, string password);
}
