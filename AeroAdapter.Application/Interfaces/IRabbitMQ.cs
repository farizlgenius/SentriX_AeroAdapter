using System;

namespace AeroAdapter.Application.Interfaces;

public interface IRabbitMQ
{
  string Host { get; }
  int Port { get; }
  string Username { get; }
  string Password { get; }
  string VirtualHost { get; }
}
