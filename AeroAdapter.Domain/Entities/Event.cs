using System;

namespace AeroAdapter.Domain.Entities;

public sealed class Event
{
  public string ModuleType { get; set; }
  public string DeviceType { get; set; }
  public string EventType { get; set; }
  public object Metadata { get; set; } = default!;
  public Event(string ModuleType, string DeviceType, string EventType, object Metadata)
  {
    this.ModuleType = ModuleType;
    this.DeviceType = DeviceType;
    this.EventType = EventType;
    this.Metadata = Metadata;
  }
}
