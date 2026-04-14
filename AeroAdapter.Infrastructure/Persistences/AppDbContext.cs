using System;
using Microsoft.EntityFrameworkCore;

namespace AeroAdapter.Infrastructure.Persistences;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

}
