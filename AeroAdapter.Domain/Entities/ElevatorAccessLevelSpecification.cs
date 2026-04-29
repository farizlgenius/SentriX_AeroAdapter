using System;

namespace AeroAdapter.Domain.Entities;

public class ElevatorAccessLevelSpecification
{

      public short ScpId {get; set;}
      public string Mac {get; set;} = string.Empty;
      public short MaxElalvl {get; set;}
      public short MaxFloors {get; set;}

      public ElevatorAccessLevelSpecification(){}

            public ElevatorAccessLevelSpecification(short scpId,string mac, short maxElalvl, short maxFloors)
      {
            ScpId = scpId;
            Mac = mac;
            MaxElalvl = maxElalvl;
            MaxFloors = maxFloors;
      }

}
