namespace StudyKafka.Domain.Enttity;

public class BankAccount
{
    public Guid Id { get; set; }
    public long AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; set; }
}