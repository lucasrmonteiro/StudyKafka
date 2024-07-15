namespace StudyKafka.Domain.Events;

public class DebitEvent : BaseEvent
{
    public decimal Amount { get; set; }
    public long AccountNumber { get; set; }
}