using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Listener;
using AeroAdapter.Infrastructure.Worker;
using AeroAdapter.Infrastructure.Writer;

namespace AeroAdapter.Api.Settings;

public class DISetting
{
      public static void DISettingHelper(WebApplicationBuilder builder)
      {
            // Repo

            // Service

            // Writer
            builder.Services.AddScoped<IDriverWriter, DriverWriter>();
            builder.Services.AddScoped<IObjectMapper, DeepReflectionMapper>();
            builder.Services.AddSingleton<AeroMessageListener>();

            // Worker
            builder.Services.AddHostedService<AeroAdapter.Infrastructure.Worker.ScpReplyWorker>();
            builder.Services.AddHostedService<RabbitMqWorker>();
      }

}
