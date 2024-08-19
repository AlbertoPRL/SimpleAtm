namespace SimpleAtm.Web.Schema.Mutation.Deposit;

public class DepositInput
{
    [GraphQLDescription("Account to deposit money")]
    public string AccountNumber { get; set; }

    [GraphQLDescription("Ammount to deposit")]
    public double Amount { get; set; }

    public DepositInput(string accountNumber, double amount)
    {
        AccountNumber = accountNumber;
        Amount = amount;
    }
}
