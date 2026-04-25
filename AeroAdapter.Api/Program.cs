
using System.Threading.Channels;
using AeroAdapter.Api.Settings;
using AeroAdapter.Application.Interfaces;
using AeroAdapter.Infrastructure.Listener;
using AeroAdapter.Infrastructure.Messaging;
using AeroAdapter.Infrastructure.Persistences;
using AeroAdapter.Infrastructure.Settings;
using AeroAdapter.Infrastructure.Worker;
using AeroAdapter.Infrastructure.Writer;
using Application.Contracts.GeneratedDtos;
using Microsoft.EntityFrameworkCore;

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
        
        //=============
        // Configuration RabbitMQ
        //=============
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

        //=============
        // Configuration Dependency Injection
        //=============
        DISetting.DISettingHelper(builder);

        // Add services to the container.
        builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("PostgresConnection"),
                    npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    ));
        

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        var readDriver = app.Services.GetRequiredService<AeroMessageListener>();
        // var writer = app.Services.GetRequiredService<ICommandWriter>();

        using (var scope = app.Services.CreateScope())
            {
                var w = scope.ServiceProvider.GetRequiredService<IDriverWriter>();
                var w2 = scope.ServiceProvider.GetRequiredService<IScpWriter>();
                // Now you can safely use sys here
                if(!w.SystemLevelSpecification())
                {
                    Console.WriteLine("Initial driver failed. Shutting down app...");
                    app.Lifetime.StopApplication(); // graceful shutdown
                }

                // Now you can safely use sys here
                if(!w2.CreateChannel())
                {
                   Console.WriteLine("Initial driver failed. Shutting down app...");
                    app.Lifetime.StopApplication(); // graceful shutdown
                }
            }

            var threadListener = new Thread(readDriver.GetTransactionUntilShutDown);
            threadListener.Start();


            app.Lifetime.ApplicationStopping.Register(async () =>
            {
          
                readDriver.SetShutDownFlag();
                Thread.Sleep(1000);
                
            });



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
