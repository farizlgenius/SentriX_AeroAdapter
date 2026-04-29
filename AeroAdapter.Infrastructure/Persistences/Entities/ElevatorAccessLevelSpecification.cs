using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class ElevatorAccessLevelSpecification : BaseEntity
{
      public short scp_id {get; set;}
      public string mac {get; set;} = string.Empty;
      public short max_ealvl {get; set;}
      public short max_floors {get; set;}

      public ElevatorAccessLevelSpecification(){}

      public ElevatorAccessLevelSpecification(Domain.Entities.ElevatorAccessLevelSpecification domain)
      {
            scp_id = domain.ScpId;
            max_ealvl = domain.MaxElalvl;
            max_floors = domain.MaxFloors;
            this.created_at = DateTime.UtcNow;
            this.updated_at = DateTime.UtcNow;
      }
}
