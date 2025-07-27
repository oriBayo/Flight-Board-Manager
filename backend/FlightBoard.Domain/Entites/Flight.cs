using FlightBoard.Domain.Enums;

namespace FlightBoard.Domain.Entities;

public class Flight
{
    public Guid Id { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTime { get; set; }
    public string Gate { get; set; } = string.Empty;
    public FlightStatus Status { get; set; }
    public string StatusString => Status.ToString();

    public FlightStatus CalculateFlightStatus(DateTime now)
    {
        var diff = DepartureTime - now;

        if (diff.TotalMinutes <= 0 && diff.TotalMinutes > -60)
            return FlightStatus.Departed;
        if (diff.TotalMinutes < 30 && diff.TotalMinutes >= 0)
            return FlightStatus.Boarding;
        if (diff.TotalMinutes <= -60)
            return FlightStatus.Landed;
        return FlightStatus.Scheduled;
    }
}