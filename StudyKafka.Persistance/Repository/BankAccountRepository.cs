using Microsoft.EntityFrameworkCore;
using StudyKafka.Domain.Enttity;
using StudyKafka.Persistance.Context;
using StudyKafka.Persistance.Interfaces;

namespace StudyKafka.Persistance.Repository;

public class BankAccountRepository : BaseRepository<BankAccount> ,IBankAccountRepository
{
    private readonly StudyKafkaContext _context;
    public BankAccountRepository(StudyKafkaContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<BankAccount?> GetByAccountNumberAsync(long accountNumber)
    {
        return await _context.Set<BankAccount>()
            .FirstOrDefaultAsync(b => b.AccountNumber == accountNumber);
    }
}