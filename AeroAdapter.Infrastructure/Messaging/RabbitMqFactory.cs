using System;
using AeroAdapter.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace AeroAdapter.Infrastructure.Messaging;


public sealed class RabbitMqFactory : IRabbitMqFactory,IDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public RabbitMqFactory(IRabbitMqOption settings)
    {
        _factory = new ConnectionFactory
        {
            HostName = settings.Host,
            Port = settings.Port,
            UserName = settings.Username,
            Password = settings.Password,
            VirtualHost = "/",
            RequestedConnectionTimeout = TimeSpan.FromSeconds(10),
            SocketReadTimeout = TimeSpan.FromSeconds(10),
            SocketWriteTimeout = TimeSpan.FromSeconds(10),
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10),
            TopologyRecoveryEnabled = true,
            
        };
    }

    public async Task<IConnection> GetConnectionAsync(CancellationToken cancellationToken = default)
    {
        if (_connection is { IsOpen: true })
            return _connection;

        await _semaphore.WaitAsync(cancellationToken);
        try
        {
            if (_connection is { IsOpen: true })
                return _connection;

            _connection = await _factory.CreateConnectionAsync(cancellationToken);
            return _connection;
        }
        catch (BrokerUnreachableException ex)
        {
            throw new Exception("RabbitMQ unreachable", ex);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        if (_connection?.IsOpen == true)
            _connection.Dispose();

        _semaphore.Dispose();
    }
}
