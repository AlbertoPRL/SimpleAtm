using HotChocolate.Authorization;
using HotChocolate.Execution.Processing;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Domain.Entities;
using SimpleAtm.Infrastructure.Data;

namespace SimpleAtm.Web.Schema.Query;

public class Query
{
    [Authorize]
    [UseProjection]
    public IQueryable<BankAccountInfo> GetBankAccounts(
        [Service] ApplicationDbContext applicationDbContext)
    {
        var bankAccounts = applicationDbContext.BankAccounts;
        var bankAccountInfos = bankAccounts.Select(ba => new BankAccountInfo
        {
            AccountNumber = ba.AccountNumber,
            Balance = ba.Balance
        });
        return bankAccountInfos;
    }

    [Authorize]
    [UseProjection]
    public BankAccountInfo GetAccountByAccountNumber(
               [Service] ApplicationDbContext applicationDbContext,
                      string accountNumber)
    {
        var bankAccount = applicationDbContext.BankAccounts
            .FirstOrDefault(ba => ba.AccountNumber == accountNumber);
        if (bankAccount == null)
        {
            throw new Exception("Bank Account not found");
        }
        return new BankAccountInfo
        {
            AccountNumber = bankAccount.AccountNumber,
            Balance = bankAccount.Balance
        };
    }
}
