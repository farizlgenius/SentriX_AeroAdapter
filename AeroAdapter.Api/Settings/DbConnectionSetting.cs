using System;
using AeroAdapter.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Api.Settings;

public class DbConnectionSetting
{
  public static void PostgresConnection(WebApplicationBuilder builder)
  {
    builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("PostgresConnection"),
                npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                ));
  }

}
