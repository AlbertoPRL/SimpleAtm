namespace SimpleAtm.Web.Schema.Mutation.Deposit;

public class DepositInput
{
    public string AccountNumber { get; set; }
    public double Amount { get; set; }

    public DepositInput(string accountNumber, double amount)
    {
        AccountNumber = accountNumber;
        Amount = amount;
    }
}
