using System;
using AeroAdapter.Application.Interfaces;

namespace AeroAdapter.Infrastructure.Settings;

public sealed class RabbitMQ : IRabbitMQ
{
  public string Host { get; set; } = string.Empty;
  public int Port { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string VirtualHost { get; set; } = string.Empty;

}
