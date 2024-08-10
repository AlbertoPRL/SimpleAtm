using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleAtm.Domain.Entities;
using SimpleAtm.Infrastructure.Identity;

namespace SimpleAtm.Infrastructure.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
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
            .HasOne<ApplicationUser>()
            .WithMany(user => user.BankAccounts)
            .HasForeignKey(ba => ba.ApplicationUserId);
    }
}
