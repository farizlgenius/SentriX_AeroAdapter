using System;
using RabbitMQ.Client;

namespace AeroAdapter.Infrastructure.Messaging;

public interface IRabbitMqFactory 
{
  Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default);
}