namespace SimpleAtm.Web.Schema.Mutation.BankAccount;

public class CreateBankAccountInput 
{
    public bool CreationConfirmed { get; set; }
    public CreateBankAccountInput(bool creationConfirmed)
    {
        CreationConfirmed = creationConfirmed;
    }
}

