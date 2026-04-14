using System.Text;
using System.Text.Json;
using AeroAdapter.Application.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AeroAdapter.Worker;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        var queueName = nameof(DeviceCreatedEvent);

        Console.WriteLine(queueName);

        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, ea) =>
        {
            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var device = JsonSerializer.Deserialize<DeviceCreatedEvent>(json);

                Console.WriteLine($"Processing Order {device?.Name}");

                await channel.BasicAckAsync(ea.DeliveryTag, false);
            }
            catch
            {
                await channel.BasicNackAsync(ea.DeliveryTag, false, true);
            }
        };

        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);
    }
}
