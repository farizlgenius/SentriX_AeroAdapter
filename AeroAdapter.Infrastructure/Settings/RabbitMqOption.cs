using System;
using AeroAdapter.Application.Interfaces;

namespace AeroAdapter.Infrastructure.Settings;

public sealed class RabbitMqOption : IRabbitMqOption
{
  public string Host { get; set; } = string.Empty;
  public int Port { get; set; }
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;

}
