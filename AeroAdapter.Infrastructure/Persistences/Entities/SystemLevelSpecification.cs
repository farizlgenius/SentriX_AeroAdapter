using System;
using System.ComponentModel.DataAnnotations;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class SystemLevelSpecification : BaseEntity
{
     public short n_ports {get;  set;} = 1024;
    public short n_scps {get;  set;} = 1024;
    public short n_timezones {get;  set;} = 0;
    public short n_holidays {get;  set;} = 0;
    public short b_direct_mode {get;  set;} = 1;
    public short debug_rq {get;  set;} = 0;
    public short n_debug_arg {get;  set;} = 0;

    public SystemLevelSpecification(){}

}
