using StudyKafka.Domain.Enttity;

namespace StudyKafka.Persistance.Interfaces;

public interface IBankAccountRepository : IBaseRepository<BankAccount>
{
    Task<BankAccount?> GetByAccountNumberAsync(long accountNumber);
}