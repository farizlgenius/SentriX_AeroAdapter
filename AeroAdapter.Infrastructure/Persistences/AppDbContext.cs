using System;
using AeroAdapter.Infrastructure.Persistences.Entities;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Persistences;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<SystemLevelSpecification> SystemLevelSpecifications {get; set;}
    public DbSet<CreateChannel> CreateChannels {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SystemLevelSpecification>()
        .HasData(
            new SystemLevelSpecification
            {
                n_ports = 1024,
                n_scps = 1024,
                n_timezones = 0,
                n_holidays = 0,
                b_direct_mode = 1,
                debug_rq = 0,
                n_debug_arg = 0,
            }
        );

        modelBuilder.Entity<CreateChannel>()
        .HasData(
            new CreateChannel
            {
                n_channel_id = 1,
                c_type = 7,
                c_port = 0,
                baudrate = 0,
                timer_1 = 3000,
                timer_2 = 0,
                c_model_id = 0,
                c_rts_mode = 0
            }
        );
    }
}
