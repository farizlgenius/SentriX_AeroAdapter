using System;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Application.Services;
using AeroAdapter.Infrastructure.Listener;
using AeroAdapter.Infrastructure.Repositories;
using AeroAdapter.Infrastructure.Worker;
using AeroAdapter.Infrastructure.Writer;

namespace AeroAdapter.Api.Settings;

public class DISetting
{
      public static void DISettingHelper(WebApplicationBuilder builder)
      {
            // Repo
            builder.Services.AddScoped<IScpRepository,ScpRepository>();
            builder.Services.AddScoped<IWriterRepository,WriterRepository>();
            builder.Services.AddScoped<IMpRepository,MpRepository>();

            // Service
            builder.Services.AddScoped<IScpService,ScpService>();

            // Writer
            builder.Services.AddScoped<IDriverWriter, DriverWriter>();
            builder.Services.AddScoped<IScpWriter,ScpWriter>();
            builder.Services.AddScoped<ISioWriter,SioWriter>();
            builder.Services.AddScoped<IMpWriter,MpWriter>();
            builder.Services.AddScoped<IObjectMapper, DeepReflectionMapper>();
            builder.Services.AddSingleton<AeroMessageListener>();

            // Worker
            builder.Services.AddHostedService<AeroAdapter.Infrastructure.Worker.ScpReplyWorker>();
            builder.Services.AddHostedService<RabbitMqWorker>();
      }

}
