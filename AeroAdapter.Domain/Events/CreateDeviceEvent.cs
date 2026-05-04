using System;

namespace AeroAdapter.Domain.Events;

public sealed record CreateDeviceEvent(
      string Name,
      int ComponentId,
      string Mac,
      string SerialNumber,
      string Ip,
      int Port,
      string Fw,
      string Type,
      DateTime SyncedAt,
      string Status,
      int LocationId
) : IRabbitMqEvent;
