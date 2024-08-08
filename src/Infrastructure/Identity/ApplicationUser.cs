using Microsoft.AspNetCore.Identity;
using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public List<BankAccount> BankAccounts { get; set; } = new();
}
