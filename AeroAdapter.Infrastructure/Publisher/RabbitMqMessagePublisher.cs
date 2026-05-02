using System;
using System.Text;
using System.Text.Json;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Domain.Constants;
using RabbitMQ.Client;

namespace AeroAdapter.Infrastructure.Messaging;

public class RabbitMqMessagePublisher(IRabbitMqFactory connection) : IMessagePublisher
{
  public async Task PublishAsync<T>(string Exchange,string RoutingKey,T Message,CancellationToken ct=default)
  {
    var conn = await connection.GetConnectionAsync();
    await using var channel = await conn.CreateChannelAsync();

    // Declate Exchange
    await channel.ExchangeDeclareAsync(
      Exchange,
      ExchangeType.Topic,
      durable:true,
      cancellationToken:ct
    );

    var body = JsonSerializer.SerializeToUtf8Bytes(Message);

    await channel.BasicPublishAsync(
      exchange: Exchange,
      routingKey: RoutingKey,
      body: body,
      cancellationToken: ct
    );
    

  }
}
