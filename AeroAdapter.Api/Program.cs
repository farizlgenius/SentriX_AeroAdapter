
using System.Threading.Channels;
using AeroAdapter.Api.Settings;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Messaging;
using AeroAdapter.Infrastructure.Settings;
using AeroAdapter.Infrastructure.Worker;
using Application.Contracts.GeneratedDtos;

namespace AeroAdapter.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //=============
        // Configuration Filerovider => provider.GetRequiredService<BackgroundWorker>()
        //=============
        ConfigurationFile.ReadConfiguration(builder);
        builder.Services.AddHostedService<AeroAdapter.Infrastructure.Worker.BackgroundWorker>();
        builder.Services.AddOptions<RabbitMqOption>()
        .Bind(builder.Configuration.GetSection("RabbitMQ"))
        .ValidateOnStart();
        builder.Services.AddScoped<IRabbitMqFactory,RabbitMqFactory>();
        builder.Services.AddSingleton<IRabbitMqOption>(sp => sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<RabbitMqOption>>().Value);
        builder.Services.AddSingleton(
                Channel.CreateBounded<SCPReplyMessageDto>(
                 new BoundedChannelOptions(10_000)
                    {
                        FullMode = BoundedChannelFullMode.DropOldest,
                        SingleReader = true,
                        SingleWriter = false
                    }
                )
             );

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
