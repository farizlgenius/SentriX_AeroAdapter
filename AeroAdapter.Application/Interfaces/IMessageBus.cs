using System;

namespace AeroAdapter.Application.Interfaces;

public interface IMessageBus
{
  Task PublishAsync<T>(T Message);
}
