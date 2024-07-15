using System.Text.Json;
using Confluent.Kafka;
using StudyKafka.Application.Interfaces;
using StudyKafka.Domain.Enttity;
using StudyKafka.Domain.Events;
using StudyKafka.Persistance.Interfaces;

namespace StudyKafka.Application.Services;

public class AccountService : IAccountService
{
    private readonly IBankAccountRepository _bankAccountRepository;
    public AccountService(IBankAccountRepository bankAccountRepository)
    {
        _bankAccountRepository = bankAccountRepository;
    }
    public async Task SaveDebitEvent(DebitEvent debitEvent)
    {
        BankAccount bankAccount = await _bankAccountRepository.GetByAccountNumberAsync(debitEvent.AccountNumber);

        if (bankAccount is null)
            throw new ArgumentException("Account Number not find");

        bankAccount.Balance += debitEvent.Amount;

        await _bankAccountRepository.UpdateAsync(bankAccount);
    }
}