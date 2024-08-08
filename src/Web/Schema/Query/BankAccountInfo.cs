using SimpleAtm.Domain.Entities;

namespace SimpleAtm.Web.Schema.Query;

public class BankAccountInfo
{
    [GraphQLDescription("Bank Account Number")]
    [ID]
    public string AccountNumber { get; set; } = null!;

    [GraphQLDescription("Current Bank Account Balance")]
    public double Balance { get; set; }
}
