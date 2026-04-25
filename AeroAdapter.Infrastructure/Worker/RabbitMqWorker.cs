using System;
using AeroAdapter.Domain.Entities;
using AeroAdapter.Domain.Helpers;
using AeroAdapter.Infrastructure.Helpers;
using AeroAdapter.Infrastructure.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AeroAdapter.Infrastructure.Worker;

public sealed class RabbitMqWorker(IServiceScopeFactory scopeFactory) : BackgroundService
{
      protected async override Task ExecuteAsync(CancellationToken stoppingToken)
      {
            while (!stoppingToken.IsCancellationRequested)
            {
                  // Message Broker
                  using var scope = scopeFactory.CreateScope();
                  var factory = scope.ServiceProvider.GetRequiredService<IRabbitMqFactory>();
                  var connection = await factory.GetConnectionAsync();
                  var channel = await connection.CreateChannelAsync();

                  await channel.ExchangeDeclareAsync("sentrix-exchange", ExchangeType.Topic, true);

                  await channel.QueueDeclareAsync("aero-device-queue", true, false, false);

                  await channel.QueueBindAsync(
                      queue: "aero-device-queue",
                      exchange: "sentrix-exchange",
                      routingKey: "device.aero.create");

                  var consumer = new AsyncEventingBasicConsumer(channel);

                  consumer.ReceivedAsync += async (sender, ea) =>
                  {
                        try
                        {
                              var message = MessageHelper.Deserialize<Event>(ea.Body.ToArray());


                              Console.WriteLine("AERO Adapter received:");
                              Console.WriteLine($"Type: {message.DeviceType}");
                              Console.WriteLine($"Event: {message.EventType}");
                              Console.WriteLine($"Metadata: {message.Metadata}");

                              await Task.Delay(500);

                              await channel.BasicAckAsync(ea.DeliveryTag, false);
                        }
                        catch (Exception ex)
                        {
                              Console.WriteLine($"Error: {ex.Message}");

                              await channel.BasicNackAsync(ea.DeliveryTag, false, false); // requeue
                        }
                  };

                  await channel.BasicConsumeAsync("aero-device-queue", false, consumer);

                  // keep worker alive forever
                  await Task.Delay(Timeout.Infinite, stoppingToken);
            }
      }
}
