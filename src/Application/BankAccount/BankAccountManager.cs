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
        var firstDigits = userId.Substring(0, 4);
        var lastDigits = userId.Substring(userId.Length - 4, 4);
        var defaulDigits = "0000";

        return Task.FromResult($"{firstDigits}{defaulDigits}{lastDigits}");
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
