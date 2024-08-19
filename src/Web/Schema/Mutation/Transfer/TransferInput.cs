namespace SimpleAtm.Web.Schema.Mutation.Transfer;

public class TransferInput
{
    [GraphQLDescription("Bank Account Number")]
    [ID]
    public string SenderAccountNumber { get; set; } = null!;

    [GraphQLDescription("Amount to transfer")]
    public double TransferAmount { get; set; }

    [GraphQLDescription("Recipient Bank Account Number")]
    [ID]
    public string RecipientAccountNumber { get; set; } = null!;

    public TransferInput(string senderAccountNumber, double transferAmount, string recipientAccountNumber)
    {
        SenderAccountNumber = senderAccountNumber;
        TransferAmount = transferAmount;
        RecipientAccountNumber = recipientAccountNumber;
    }
}
