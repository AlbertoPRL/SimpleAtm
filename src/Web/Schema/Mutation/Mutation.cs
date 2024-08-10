using HotChocolate.Authorization;
using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Web.Schema.Mutation.BankAccount;
using SimpleAtm.Web.Schema.Mutation.Login;
using SimpleAtm.Web.Schema.Mutation.User;

namespace SimpleAtm.Web.Schema.Mutation;

public class Mutation
{
    public async Task<CreateUserAccountPayload> CreateAccount(
        CreateUserAccountInput input,
        [Service] IIdentityService identityService)
    {
        var response = await identityService.CreateUserAsync(input.UserName, input.Password);
        if(response.Result.Succeeded)
        {
            return new CreateUserAccountPayload(200, response.Result.Errors, true, response.UserId);
        }
        else 
        {
            return new CreateUserAccountPayload(400, response.Result.Errors, false, null);
        }
    }

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

    [Authorize]
    public async Task<CreateBankAccountPayload> CreateBankAccount(
        CreateBankAccountInput input,
        [Service] IBankAccountRepository bankAccountRepository,
        [Service]ICurrentUser user,
        [Service] IBankAccountManager bankAccountManager)
    {
        var userId = user.Id;
        if (userId != null)
        {
            var accountNumber = bankAccountManager.GenerateAccountNumber();
            var balance = bankAccountManager.GetInitialBalance();
            var result = await bankAccountRepository.CreateBankAccount(accountNumber, balance);
            if (result.Succeeded)
            {
                return new CreateBankAccountPayload(accountNumber, balance,200, result.Succeeded);
            }
            else
            {
                return new CreateBankAccountPayload("No user id Found", 0, 400, result.Succeeded);
            }
        }
        return new CreateBankAccountPayload(string.Empty, 0, 400, false);
    }

    //[Authorize]
    //[UseProjection]
    //public async Task<DepositPayload> Deposit
}
