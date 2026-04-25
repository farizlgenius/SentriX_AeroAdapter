using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class Scp : BaseEntity
{
      public short scp_number {get; set;}
      public string mac {get; set;} = string.Empty;
    
      public Scp(){}
}
