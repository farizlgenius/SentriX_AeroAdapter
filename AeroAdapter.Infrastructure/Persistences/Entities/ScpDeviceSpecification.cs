using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public class ScpDeviceSpecification : BaseEntity
{
       public short scp_id {get; set;}
       public string mac {get; set;} = string.Empty;
      public short n_msp1_port {get; set;} 
      public int n_transcations {get; set;} 
      public short n_sio {get; set;} 
      public short n_mp {get; set;}
      public short n_cp {get; set;}
      public short n_acr {get; set;}
      public short n_alvl {get; set;}
      public short n_trgr {get; set;}
      public short n_proc {get; set;}
      public short gmt_offset {get; set;}
      public short n_dst_id {get; set;}
      public short n_tz {get; set;}
      public short n_hol {get; set;}
      public short n_mpg {get; set;}
      public int n_tran_limit {get; set;}
      public short n_oper_mode {get; set;}
      public short oper_type {get; set;} = 1;
      public short n_language {get; set;} = 0;

      public ScpDeviceSpecification(){}
}
