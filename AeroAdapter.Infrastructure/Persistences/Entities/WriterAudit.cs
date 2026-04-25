using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public class WriterAudit : BaseEntity
{
      public string mac {get; set;} = string.Empty;
      public int scp_id {get; set;}
      public int command_code {get; set;}
      public string command {get; set;} = string.Empty;
      public int tag {get; set;}
      public DateTime send_at {get; set;}
      public DateTime received_at {get; set;}
      public string body {get; set;}  =string.Empty;
      public string status {get; set;} = string.Empty;
      public bool is_nak {get; set;}
      public string reason {get; set;} = string.Empty;

      public WriterAudit(){}
}
