using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAtm.Domain.Entities;
public class BankAccount : BaseEntity
{
    public long AccountNumber { get; set; }
    public float Balance { get; set; }
    public int Pin { get; set; }
}
