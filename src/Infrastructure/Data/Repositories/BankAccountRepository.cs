using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Application.Common.Models;
using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Infrastructure.Data.Repositories;
public class BankAccountRepository : IBankAccountRepository
{
    private readonly ApplicationDbContext _context;
    public BankAccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateBankAccount(string accountNumber, double balance, Guid userId)
    {
        var bankAccount = new BankAccount
        {
            AccountNumber = accountNumber,
            Balance = balance,
            ApplicationUserId = userId           
        };
        var result = _context.BankAccounts.Add(bankAccount);     
        if (result != null)
        {
            await _context.SaveChangesAsync();
            return Result.Success();
        }
        return Result.Failure(new List<string> { "Failed to create bank account" });
    }

    public Task<List<Result>> GetAllBankAccounts()
    {
        throw new NotImplementedException();
    }

    public Task<Result> GetBankAccountById(int id)
    {
        throw new NotImplementedException();
    }
}
