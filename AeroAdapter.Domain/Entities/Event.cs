using System;

namespace AeroAdapter.Domain.Entities;

public sealed class Event
{
  public string ModuleType { get; set; }
  public string DeviceType { get; set; }
  public string EventType { get; set; }
  public object Metadata { get; set; } = default!;
  public Event(string module, string device, string even, object metadata)
  {
    this.ModuleType = module;
    this.DeviceType = device;
    this.EventType = even;
    this.Metadata = metadata;
  }
}
