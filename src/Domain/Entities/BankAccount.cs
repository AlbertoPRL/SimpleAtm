namespace SimpleAtm.Domain.Entities;

public class BankAccount : BaseEntity
{
    public Guid ApplicationUserId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public double Balance { get; set; }
}
