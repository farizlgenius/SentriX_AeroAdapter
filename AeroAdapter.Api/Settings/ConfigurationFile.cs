using System;
using AeroAdapter.Application.Interfaces;

namespace AeroAdapter.Api.Settings;

public class ConfigurationFile
{
  public static void ReadConfiguration(WebApplicationBuilder builder)
  {
    builder.Services
           .AddOptions<IRabbitMQ>()
           .Bind(builder.Configuration.GetSection("RabbitMQ"))
           .ValidateOnStart();

    builder.Services.AddSingleton<IRabbitMQ>(sp => sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<AeroAdapter.Infrastructure.Settings.RabbitMQ>>().Value);
  }
}
