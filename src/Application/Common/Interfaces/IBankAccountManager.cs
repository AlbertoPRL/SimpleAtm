using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAtm.Application.Common.Interfaces;
public interface IBankAccountManager
{
   Task<string> GenerateAccountNumber(string userId);
   Task<double> GetInitialBalance();
}
