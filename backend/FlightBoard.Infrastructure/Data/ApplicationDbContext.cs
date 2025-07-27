
using FlightBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightBoard.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Flight> Flights { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
