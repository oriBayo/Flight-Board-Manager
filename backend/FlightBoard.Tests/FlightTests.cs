using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;
using Xunit.Abstractions;

namespace FlightBoard.Tests;

public class FlightTests
{

    private readonly ITestOutputHelper _output;

    public FlightTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData(90, FlightStatus.Scheduled)]
    [InlineData(20, FlightStatus.Boarding)]
    [InlineData(-20, FlightStatus.Departed)]
    [InlineData(-80, FlightStatus.Landed)]
    public void CalculateFlightStatus_ReturnExpectedStatus(double minutesFromNow, FlightStatus expectedStatus)
    {
        // Arrange
        var flight = new Flight
        {
            DepartureTime = DateTime.UtcNow.AddMinutes(minutesFromNow)
        };

        // Act
        var status = flight.CalculateFlightStatus(DateTime.UtcNow);

        // Assert
        Assert.Equal(expectedStatus, status);
    }

}