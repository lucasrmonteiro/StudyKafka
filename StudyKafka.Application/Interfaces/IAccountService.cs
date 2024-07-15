using StudyKafka.Domain.Events;

namespace StudyKafka.Application.Interfaces;

public interface IAccountService
{
    Task SaveDebitEvent(DebitEvent debitEvent);
}