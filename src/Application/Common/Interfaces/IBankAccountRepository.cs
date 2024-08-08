using SimpleAtm.Application.Common.Models;
using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Application.Common.Interfaces;
public interface IBankAccountRepository
{
    Task<Result> CreateBankAccount(string accountNumber, double balance);
    Task<Result> GetBankAccountById(int id);
    Task<List<Result>> GetAllBankAccounts();
}
