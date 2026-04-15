using System;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public sealed class CreateChannel : BaseEntity
{
    public short n_channel_id {get; set;}
    public short c_type {get; set;}
    public short c_port {get; set;}
    public short baudrate {get; set;}
    public short timer_1 {get; set;} 
    public short timer_2 {get; set;}
    public short c_model_id {get; set;}
    public short c_rts_mode {get; set;}

    public CreateChannel(){}

}
