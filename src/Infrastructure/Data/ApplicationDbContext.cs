using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleAtm.Application.BankAccount;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Domain.Entities;
using SimpleAtm.Infrastructure.Identity;

namespace SimpleAtm.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<BankAccount> BankAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Entity<BankAccount>()
                .HasKey(bankAccount => bankAccount.AccountNumber);
    }
}
