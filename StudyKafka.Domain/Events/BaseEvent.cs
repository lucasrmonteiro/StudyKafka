namespace StudyKafka.Domain.Events;

public class BaseEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
}