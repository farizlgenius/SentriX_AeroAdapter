using System;
using AeroAdapter.Domain.Enums;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class Scp : BaseEntity
{
      public short scp_id {get; set;}
      public string mac {get; set;} = string.Empty;
    
      public Scp(){}

      public Scp(short ScpId,string Mac)
      {
            this.scp_id = ScpId;
            this.mac = Mac;
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
      }

      public void UpdateScpId(short scp_number)
      {
            this.scp_id = scp_number;
            this.updated_at = DateTime.UtcNow;
      }


      public void Update(short ScpId,string Mac)
      {
            this.scp_id = ScpId;
            this.mac = Mac;
            this.updated_at = DateTime.UtcNow;
      }
}
