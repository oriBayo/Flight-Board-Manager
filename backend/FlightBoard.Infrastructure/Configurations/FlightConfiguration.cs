using FlightBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBoard.Infrastructure.Configurations;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.HasKey(f => f.Id);
        builder.Property(f => f.FlightNumber).IsRequired();
        builder.Property(f => f.Destination).IsRequired();
        builder.Property(f => f.DepartureTime).IsRequired();
        builder.Property(f => f.Gate).IsRequired();
        builder.Property(f => f.Status).IsRequired();
    }
}
