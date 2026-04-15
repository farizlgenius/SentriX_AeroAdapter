using System;

namespace AeroAdapter.Application.Interfaces;

public interface IRabbitMqOption
{
  string Host { get; }
  int Port { get; }
  string Username { get; }
  string Password { get; }
}
