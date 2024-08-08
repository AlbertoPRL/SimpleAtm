using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleAtm.Application.Common.Interfaces;

namespace SimpleAtm.Application.BankAccount;
public class BankAccountManager : IBankAccountManager
{
    private readonly double InitialBalance = 100.00;
    public Task<string> GenerateAccountNumber(string userId)
    {
        var newGuid = Guid.NewGuid().ToString();
        var defaultDigits = newGuid.Substring(0, 8);
        var firstDigits = userId.Substring(0, 4);
        var middleDigits = "0000";
        var lastDigits = userId.Substring(userId.Length - 4, 4);
        
        return Task.FromResult($"{firstDigits}{defaultDigits}{middleDigits}{lastDigits}");
    }

    public Task<double> GetInitialBalance()
    {
        return Task.FromResult(InitialBalance);
    }

    public Task<double> CheckBalance()
    {
        return Task.FromResult(0.00);
    }
}
