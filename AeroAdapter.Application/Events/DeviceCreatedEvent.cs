using System;

namespace AeroAdapter.Application.Events;

public sealed record DeviceCreatedEvent(int Id, string Name, string Mac);
