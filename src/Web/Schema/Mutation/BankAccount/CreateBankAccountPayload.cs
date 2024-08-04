namespace SimpleAtm.Web.Schema.Mutation.BankAccount;

public class CreateBankAccountPayload
{
    public string AccountNumber { get; set; }
    public double Balance { get; set; }
    public int Code { get; set; }
    public string[]? Messages { get; set; }
    public bool Success { get; set; }

    public CreateBankAccountPayload(string accountNumber, double balance, int code, bool success)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        Code = code;
        Success = success;
    }
}
