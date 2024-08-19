namespace SimpleAtm.Web.Schema.Mutation.Deposit;

public class DepositPayload
{
    [GraphQLDescription("The account number of the account that was deposited into")]
    public string AccountNumber { get; set; }

    [GraphQLDescription("The amount after the deposit")]
    public double AccountBalance { get; set; }

    [GraphQLDescription("Errors")]
    public string? Messages { get; set; }

    [GraphQLDescription("Success status")]
    public bool Success { get; set; }

    public DepositPayload(string accountNumber, double accountBalance, bool success, string? message)
    {
        AccountNumber = accountNumber;
        AccountBalance = accountBalance;
        Success = success;
        Messages = message;
    }
}
