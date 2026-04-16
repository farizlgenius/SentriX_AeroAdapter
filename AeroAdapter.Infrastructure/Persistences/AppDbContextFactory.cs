using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AeroAdapter.Infrastructure.Persistences;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=SentriXAero;Username=postgres;Password=password;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
