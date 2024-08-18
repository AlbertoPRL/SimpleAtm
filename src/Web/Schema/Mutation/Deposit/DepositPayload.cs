namespace SimpleAtm.Web.Schema.Mutation.Deposit;

public class DepositPayload
{
    [GraphQLDescription("The account number of the account that was deposited into")]
    public string AccountNumber { get; set; }

    [GraphQLDescription("The amount deposited")]
    public double Amount { get; set; }

    [GraphQLDescription("Errors")]
    public string[] Messages { get; set; }

    [GraphQLDescription("Success status")]
    public bool Success { get; set; }

    public DepositPayload(string accountNumber, double amount, string[] messages, bool success)
    {
        AccountNumber = accountNumber;
        Amount = amount;
        Messages = messages;
        Success = success;
    }
}
