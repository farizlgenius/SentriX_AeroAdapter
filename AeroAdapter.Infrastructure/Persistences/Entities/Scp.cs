using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class Scp : BaseEntity
{
      public short scp_number {get; set;}
      public string mac {get; set;} = string.Empty;
    
      public Scp(){}

      public Scp(short scp_number, string mac)
      {
            this.scp_number = scp_number;
            this.mac = mac;
      }

      public void Update(short scp_number)
      {
            this.scp_number = scp_number;
            this.updated_at = DateTime.UtcNow;
      }
}
