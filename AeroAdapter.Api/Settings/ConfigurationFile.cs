using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Settings;

namespace AeroAdapter.Api.Settings;

public class ConfigurationFile
{
  public static void ReadConfiguration(WebApplicationBuilder builder)
  {
    builder.Services
           .AddOptions<RabbitMqOption>()
           .Bind(builder.Configuration.GetSection("RabbitMQ"))
           .ValidateOnStart();

    builder.Services.AddSingleton<IRabbitMqOption>(sp => sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<AeroAdapter.Infrastructure.Settings.RabbitMqOption>>().Value);
  }
}
