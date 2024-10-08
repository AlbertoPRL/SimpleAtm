﻿using SimpleAtm.Application.Common.Models;
using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Application.Common.Interfaces;
public interface IBankAccountRepository
{
    Task<Result> CreateBankAccount(string accountNumber, double balance, Guid userId);
    Task<Result> GetBankAccountByAccountNumber(Guid id);
    Task<List<Result>> GetAllBankAccounts();
}
