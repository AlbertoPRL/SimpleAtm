using HotChocolate.Authorization;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Infrastructure.Data;
using SimpleAtm.Web.Schema.Mutation.BankAccount;
using SimpleAtm.Web.Schema.Mutation.Deposit;
using SimpleAtm.Web.Schema.Mutation.Login;
using SimpleAtm.Web.Schema.Mutation.User;
using SimpleAtm.Web.Schema.Mutation.Withdraw;

namespace SimpleAtm.Web.Schema.Mutation;

public class Mutation
{
    [GraphQLDescription("Create a user account")]
    public async Task<CreateUserAccountPayload> CreateAccount(
        CreateUserAccountInput input,
        [Service] IIdentityService identityService)
    {
        var response = await identityService.CreateUserAsync(input.UserName, input.Password);
        if (response.Result.Succeeded)
        {
            return new CreateUserAccountPayload(200, response.Result.Errors, true, response.UserId);
        }
        else
        {
            return new CreateUserAccountPayload(400, response.Result.Errors, false, null);
        }
    }

    [GraphQLDescription("Login to the application")]
    public async Task<LoginPayload> Login(
        LoginInput input,
        [Service] IIdentityService identityService)
    {
        var result = await identityService.SignInAsync(input.UserName, input.Password);
        if (result.Succeeded)
        {
            return new LoginPayload(200, result.Errors, true, result.Token);
        }
        return new LoginPayload(400, result.Errors, false, result.Token);
    }

    [GraphQLDescription("Create a bank account")]
    [Authorize]
    public async Task<CreateBankAccountPayload> CreateBankAccount(
        CreateBankAccountInput input,
        [Service] IBankAccountRepository bankAccountRepository,
        [Service] ICurrentUser user,
        [Service] IBankAccountManager bankAccountManager)
    {
        var userId = user.Id;
        if (userId != Guid.Empty)
        {
            var accountNumber = bankAccountManager.GenerateAccountNumber();
            var balance = bankAccountManager.GetInitialBalance();
            var result = await bankAccountRepository.CreateBankAccount(accountNumber, balance, userId);
            if (result.Succeeded)
            {
                return new CreateBankAccountPayload(accountNumber, balance, 200, result.Succeeded);
            }
            else
            {
                return new CreateBankAccountPayload("No user id Found", 0, 400, result.Succeeded);
            }
        }
        return new CreateBankAccountPayload(string.Empty, 0, 400, false);
    }

    [GraphQLDescription("Deposit money into a bank account")]
    [Authorize]
    public async Task<DepositPayload> Deposit(
        DepositInput input,
        [Service] ICurrentUser currentUser,
        [Service] ApplicationDbContext context)
    {
        try
        {
            var account = context.BankAccounts.FirstOrDefault(x => x.AccountNumber == input.AccountNumber);
            if (account != null)
            {
                account.Balance += input.Amount;
                await context.SaveChangesAsync();
                return new DepositPayload(account.AccountNumber, account.Balance, true, string.Empty);
            }
            return new DepositPayload(string.Empty, 0, false, "Account not found");
        }
        catch (Exception ex)
        {
            return new DepositPayload(string.Empty, 0, false, ex.Message);
        }
    }

    [Authorize]
    public async Task<WithdrawPayload> Withdraw(
        WithdrawInput input,
        [Service] ICurrentUser currentUser,
        [Service] ApplicationDbContext context)
    {
        try
        {
            var account = context.BankAccounts.FirstOrDefault(x => x.AccountNumber == input.AccountNumber);
            if (account != null)
            {
                if (account.Balance >= input.Amount)
                {
                    account.Balance -= input.Amount;
                    await context.SaveChangesAsync();
                    return new WithdrawPayload(account.AccountNumber, account.Balance, true, string.Empty);
                }
                return new WithdrawPayload(account.AccountNumber, account.Balance, false, "Insufficient funds");
            }
            return new WithdrawPayload(string.Empty, 0, false, "Account not found");
        }
        catch (Exception ex)
        {
            return new WithdrawPayload(string.Empty, 0, false, ex.Message);
        }
    }
}
