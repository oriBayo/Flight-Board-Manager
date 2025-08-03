using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Enums;

namespace FlightBoard.Tests;

public class FlightTests
{
    [Theory]
    [InlineData(90, FlightStatus.Scheduled)]
    [InlineData(20, FlightStatus.Boarding)]
    [InlineData(-20, FlightStatus.Departed)]
    [InlineData(-80, FlightStatus.Landed)]
    public void CalculateFlightStatus_ReturnExpectedStatus(double minutesFromNow, FlightStatus expectedStatus)
    {
        // Given
        var flight = new Flight
        {
            DepartureTime = DateTime.UtcNow.AddMinutes(minutesFromNow)
        };

        // When
        flight.CalculateFlightStatus(DateTime.UtcNow);

        // Then
        Assert.Equal(expectedStatus, flight.Status);
    }

}