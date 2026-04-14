using System;
using System.Text;
using System.Text.Json;
using AeroAdapter.Application.Interfaces;
using RabbitMQ.Client;

namespace AeroAdapter.Infrastructure.Messaging;

public class RabbitMqMessageBus(IRabbitMqPersistenceConnection connection) : IMessageBus
{
  public async Task PublishAsync<T>(T Message)
  {
    var conn = await connection.GetConnectionAsync();
    await using var channel = await conn.CreateChannelAsync();

    var queueName = typeof(T).Name;

    Console.WriteLine(queueName);

    await channel.QueueDeclareAsync(
        queue: queueName,
        durable: true,
        exclusive: false,
        autoDelete: false,
        arguments: null);

    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Message));

    await channel.BasicPublishAsync(
        exchange: "",
        routingKey: queueName,
        body: body
        );

  }
}
