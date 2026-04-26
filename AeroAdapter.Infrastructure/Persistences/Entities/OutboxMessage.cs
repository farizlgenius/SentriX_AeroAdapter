using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class OutboxMessage
{
      public OutboxMessage(int id, string type, string payload, DateTime occurred_at, DateTime? processed_at, string? error)
      {
            this.id = id;
            this.type = type;
            this.payload = payload;
            this.occurred_at = occurred_at;
            this.processed_at = processed_at;
            this.error = error;
      }

      public int id { get; set; }
  public string type { get; set; } = string.Empty;
  public string payload { get; set; } = string.Empty;
  public DateTime occurred_at { get; set; }
  public DateTime? processed_at { get; set; }
  public string? error { get; set; }

}
