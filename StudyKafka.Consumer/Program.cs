using Microsoft.EntityFrameworkCore;
using StudyKafka.Application.Interfaces;
using StudyKafka.Application.Services;
using StudyKafka.Consumer;
using StudyKafka.Persistance.Context;
using StudyKafka.Persistance.Interfaces;
using StudyKafka.Persistance.Repository;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContextFactory<StudyKafkaContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IBankAccountRepository ,BankAccountRepository>(it =>
{
    var dbContextFactory = it.GetRequiredService<IDbContextFactory<StudyKafkaContext>>();
    var dbContext = dbContextFactory.CreateDbContext();
    return new BankAccountRepository(dbContext);
});
builder.Services.AddTransient<IAccountService ,AccountService>();
builder.Services.AddHostedService<Worker>();



var host = builder.Build();
host.Run();