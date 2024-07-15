using System.Text.Json;
using Confluent.Kafka;
using StudyKafka.Application.Interfaces;
using StudyKafka.Domain.Events;

namespace StudyKafka.Consumer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IAccountService _accountService;

    public Worker(ILogger<Worker> logger ,IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var config = new ConsumerConfig
            {
                GroupId = "account-consumer-group",
                BootstrapServers = "kafka:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("account-topic");

                try
                {
                        
                    while (true)
                    {
                        var consumeResult = consumer.Consume();
                        Console.WriteLine($"Consumed message '{consumeResult.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                        var debitEvent = JsonSerializer.Deserialize<DebitEvent>(consumeResult.Value);
                        await _accountService.SaveDebitEvent(debitEvent);
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}