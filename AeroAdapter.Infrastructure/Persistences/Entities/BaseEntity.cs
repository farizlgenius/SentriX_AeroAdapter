using System;
using System.ComponentModel.DataAnnotations;

namespace AeroAdapter.Infrastructure.Persistences.Entities;

public class BaseEntity
{
    [Key]
    public int id {get; set;}
    public DateTime created_at {get; set;} = DateTime.UtcNow;
    public DateTime updated_at {get; set;} = DateTime.UtcNow;
}
