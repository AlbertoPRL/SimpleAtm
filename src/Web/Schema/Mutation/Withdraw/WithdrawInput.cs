namespace SimpleAtm.Web.Schema.Mutation.Withdraw;

public class WithdrawInput
{
    [GraphQLDescription("Bank Account Number")]
    [ID]
    public string AccountNumber { get; set; } = null!;

    [GraphQLDescription("Amount to withdraw")]
    public double Amount { get; set; }

    public WithdrawInput(string accountNumber, double amount)
    {
        AccountNumber = accountNumber;
        Amount = amount;
    }
}
