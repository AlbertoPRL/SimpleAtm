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
    public string GenerateAccountNumber()
    {
        return Guid.NewGuid().ToString();
    }
    public double GetInitialBalance()
    {
        return InitialBalance;
    }

    public Task<double> CheckBalance()
    {
        return Task.FromResult(0.00);
    }
}
