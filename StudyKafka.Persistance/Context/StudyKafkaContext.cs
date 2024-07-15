using Microsoft.EntityFrameworkCore;
using StudyKafka.Domain.Enttity;

namespace StudyKafka.Persistance.Context;

public class StudyKafkaContext : DbContext
{
    public DbSet<BankAccount> BankAccount { get; set; }
    public StudyKafkaContext(DbContextOptions<StudyKafkaContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.AccountNumber).IsRequired();
            entity.Property(e => e.AccountHolder).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18, 2)");
            
            entity.HasData(new BankAccount
            {
                Id = Guid.NewGuid(),
                AccountNumber = 123456,
                AccountHolder = "Tiquinho Soares",
                Balance = 0m
            });
        });
    }
    
}