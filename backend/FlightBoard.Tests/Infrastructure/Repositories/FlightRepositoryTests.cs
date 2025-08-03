using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;
using FlightBoard.Domain.Exceptions;
using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Data;
using FlightBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FlightBoard.Tests.Infrastructure.Repositories;

public class FlightRepositoryTests : IDisposable
{
    private readonly DbContextOptions<ApplicationDbContext>? _options;
    private readonly ApplicationDbContext _context;
    private readonly IFlightRepository _repository;

    public FlightRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        _context = new ApplicationDbContext(_options);
        _repository = new FlightRepository(_context);
    }

    private async Task CreateFlightsInContext()
    {
        var flights = new[]
                {
            new Flight { Id = Guid.NewGuid(), FlightNumber = "LY101", Destination = "LA", DepartureTime = DateTime.UtcNow.AddMinutes(45), Gate = "A1", Status = FlightStatus.Scheduled },
            new Flight { Id = Guid.NewGuid(), FlightNumber = "LY102", Destination = "NY", DepartureTime = DateTime.UtcNow.AddMinutes(15), Gate = "B2", Status = FlightStatus.Boarding },
            new Flight { Id = Guid.NewGuid(), FlightNumber = "LY103", Destination = "LA", DepartureTime = DateTime.UtcNow.AddMinutes(-75), Gate = "C3", Status = FlightStatus.Landed }
        };
        _context.AddRange(flights);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetAllFlightsAsync_ShouldReturnFlights()
    {
        // Given
        _context.Flights.Add(new Flight { FlightNumber = "LY101", DepartureTime = DateTime.Now.AddHours(1) });
        await _context.SaveChangesAsync();

        // When
        var result = await _repository.GetAllFlightsAsync();

        // Then
        Assert.Single(result);
    }

    [Fact]
    public async Task CreateFlightAsync_ShouldAddFlightToDatabase()
    {
        // Given

        var flight = new Flight
        {
            FlightNumber = "LY101",
            DepartureTime = DateTime.Now.AddMinutes(1),
            Destination = "LA",
            Gate = "A3"
        };
        flight.CalculateFlightStatus(DateTime.Now);

        // When
        await _repository.CreateFlightAsync(flight);

        // Then
        var savedFlight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightNumber == "LY101");

        Assert.NotNull(savedFlight);
        Assert.Equal("LY101", savedFlight.FlightNumber);
        Assert.Equal(flight.DepartureTime, savedFlight.DepartureTime);
        Assert.Equal("LA", savedFlight.Destination);
        Assert.Equal("A3", savedFlight.Gate);
        Assert.Equal(FlightStatus.Boarding, savedFlight.Status);
        Assert.Equal("Boarding", savedFlight.StatusString);
    }

    [Fact]
    public async Task DeleteFlightAsync_ExistingFlight_ShouldRemoveFlightFromDatabase()
    {
        // Given
        var flight = new Flight
        {
            Id = Guid.NewGuid(),
            FlightNumber = "LY101",
            DepartureTime = DateTime.UtcNow.AddMinutes(1),
            Destination = "LA",
            Gate = "A3"
        };
        _context.Add(flight);
        await _context.SaveChangesAsync();

        // When
        await _repository.DeleteFlightAsync(flight.Id);

        // Then
        var savedFlight = await _context.Flights.FindAsync(flight.Id);
        Assert.Null(savedFlight);
    }

    [Fact]
    public async Task DeleteFlightAsync_NonExistingFlight_ShouldThrowFlightNotFoundException()
    {
        // Given
        var nonExistingId = Guid.NewGuid();

        // Then
        await Assert.ThrowsAsync<FlightNotFoundException>(() => _repository.DeleteFlightAsync(nonExistingId));
    }

    [Fact]
    public async Task FlightNumberExistsAsync_ExistingFlightNumber_ShouldReturnTrue()
    {
        // Given
        var flight = new Flight
        {
            Id = Guid.NewGuid(),
            FlightNumber = "LY101",
            DepartureTime = DateTime.Now.AddMinutes(1),
            Destination = "LA",
            Gate = "A3"
        };
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        // When
        var result = await _repository.FlightNumberExistsAsync("LY101");

        // Then
        Assert.True(result);
    }

    [Fact]
    public async Task FlightNumberExistsAsync_NonExistingFlightNumber_ShouldReturnFalse()
    {
        // When
        var result = await _repository.FlightNumberExistsAsync("LY999");

        // Then
        Assert.False(result);
    }

    [Fact]
    public async Task SearchFlightsAsync_NoFilters_ShouldReturnAllFlights()
    {
        // Given
        await CreateFlightsInContext();

        // When
        var result = await _repository.SearchFlightsAsync(null, null);

        // Then
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task SearchFlightsAsync_FilterByStatus_ShouldReturnMatchingFlights()
    {
        // Given
        await CreateFlightsInContext();

        // When
        var result = await _repository.SearchFlightsAsync("Boarding", null);

        // Then
        Assert.Single(result);
        Assert.All(result, f => Assert.Equal(FlightStatus.Boarding, f.Status));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
