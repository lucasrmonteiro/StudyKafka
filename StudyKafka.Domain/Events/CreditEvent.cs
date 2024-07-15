namespace StudyKafka.Domain.Events;

public class CreditEvent : BaseEvent
{
    public double Amount { get; set; }
    public long AccountNumber { get; set; }
}