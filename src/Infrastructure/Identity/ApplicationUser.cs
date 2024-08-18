using Microsoft.AspNetCore.Identity;
using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Infrastructure.Identity;
public class ApplicationUser : IdentityUser<Guid>, IUser
{
    public  List<BankAccount>? BankAccounts { get; set; }
}

