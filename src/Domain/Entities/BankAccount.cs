using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAtm.Domain.Entities;
public class BankAccount : BaseEntity
{
    public string AccountNumber { get; set; } = null!;
    public double Balance { get; set; }
}
