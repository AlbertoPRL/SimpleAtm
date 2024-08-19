namespace SimpleAtm.Web.Schema.Mutation.Withdraw;

public class WithdrawPayload
{
    [GraphQLDescription("Bank Account Number")]
    public string AccountNumber { get; set; } = null!;

    [GraphQLDescription("Current Bank Account Balance")]
    public double Balance { get; set; }

    [GraphQLDescription("Success")]
    public bool Succeeded { get; set; }

    [GraphQLDescription("Error")]
    public string? Error { get; set; }


    public WithdrawPayload(string accountNumber, double balance, bool succeeded, string? error)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        Succeeded = succeeded;
        Error = error;
    }
}
