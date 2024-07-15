using System.Text.Json;
using Confluent.Kafka;
using StudyKafka.Domain.Events;

namespace StudyKafka.Producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    private ProducerConfig _config = new ProducerConfig(new ClientConfig()
    {
        BootstrapServers = "kafka:9092",
        ClientId = "StudyKafka.Producer",
        Acks = Acks.None
    });

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                using (var producer = new ProducerBuilder<Null, string>(_config).Build())
                {
                    var debitEvent = new DebitEvent()
                    {
                        AccountNumber = 123456,
                        Amount = 100
                    };
                    string json = JsonSerializer.Serialize(debitEvent);
                    var deliveryResult = await producer.ProduceAsync("account-topic", new Message<Null, string> { Value = json });
                }
            }

            await Task.Delay(120000, stoppingToken);
        }
    }
}