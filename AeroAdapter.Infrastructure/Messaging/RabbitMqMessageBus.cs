using System;
using System.Text;
using System.Text.Json;
using AeroAdapter.Application.Interfaces;
using RabbitMQ.Client;

namespace AeroAdapter.Infrastructure.Messaging;

public class RabbitMqMessageBus : IMessageBus
{
  private readonly ConnectionFactory _factory;

  public RabbitMqMessageBus()
  {
    _factory = new ConnectionFactory()
    {
      HostName = "localhost"
    };
  }
  public async Task PublishAsync<T>(T Message)
  {
    using var connection = await _factory.CreateConnectionAsync();
    await using var channel = await connection.CreateChannelAsync();

    await channel.QueueDeclareAsync(
            queue: "order-created",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    var json = JsonSerializer.Serialize(Message);
    var body = Encoding.UTF8.GetBytes(json);

    await channel.BasicPublishAsync(
        exchange: "",
        routingKey: "order-created",
        body: body
        );

  }
}
