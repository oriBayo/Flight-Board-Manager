using System;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;

namespace FlightBoard.Infrastructure.Data;

public class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Flights.Any())
            return;

        var flights = new List<Flight>
{
    new Flight
    {
        Id = Guid.NewGuid(),
        FlightNumber = "EL123",
        Destination = "New York",
        DepartureTime = DateTime.Parse("2025-07-22T14:30:00Z").ToUniversalTime(),
        Gate = "A12",
        Status = FlightStatus.Boarding
    },
    new Flight
    {
        Id = Guid.NewGuid(),
        FlightNumber = "BA456",
        Destination = "London",
        DepartureTime = DateTime.Parse("2025-07-22T16:45:00Z").ToUniversalTime(),
        Gate = "B8",
        Status = FlightStatus.Scheduled
    },
    new Flight
    {
        Id = Guid.NewGuid(),
        FlightNumber = "LH789",
        Destination = "Frankfurt",
        DepartureTime = DateTime.Parse("2025-07-22T12:15:00Z").ToUniversalTime(),
        Gate = "C5",
        Status = FlightStatus.Departed
    },
    new Flight
    {
        Id = Guid.NewGuid(),
        FlightNumber = "AF321",
        Destination = "Paris",
        DepartureTime = DateTime.Parse("2025-07-22T10:30:00Z").ToUniversalTime(),
        Gate = "A3",
        Status = FlightStatus.Landed
    },
    new Flight
    {
        Id = Guid.NewGuid(),
        FlightNumber = "DL654",
        Destination = "Los Angeles",
        DepartureTime = DateTime.Parse("2025-07-22T18:20:00Z").ToUniversalTime(),
        Gate = "D11",
        Status = FlightStatus.Scheduled
    }
};

        context.Flights.AddRange(flights);
        context.SaveChanges();
    }
}
