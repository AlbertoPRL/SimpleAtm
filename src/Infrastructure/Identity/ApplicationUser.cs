using Microsoft.AspNetCore.Identity;

namespace SimpleAtm.Infrastructure.Identity;
public class ApplicationUser : IdentityUser
{
    public long AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int Pin { get; set; }
}
