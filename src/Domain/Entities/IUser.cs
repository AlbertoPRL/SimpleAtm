namespace SimpleAtm.Domain.Entities;
public interface IUser
{
    public List<BankAccount>? BankAccounts { get; set; }
}
