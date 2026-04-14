using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class OutboxMessage
{
  public int id { get; set; }
  public string type { get; set; } = string.Empty;
  public string payload { get; set; } = string.Empty;
  public DateTime occurred_at { get; set; }
  public DateTime? processed_at { get; set; }
  public string? error { get; set; }

}
