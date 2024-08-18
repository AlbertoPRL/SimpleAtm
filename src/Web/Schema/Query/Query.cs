using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotChocolate.Authorization;
using Microsoft.IdentityModel.Tokens;
using SimpleAtm.Infrastructure.Data;

namespace SimpleAtm.Web.Schema.Query;

public class Query
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration = null!;
    public Query(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

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
