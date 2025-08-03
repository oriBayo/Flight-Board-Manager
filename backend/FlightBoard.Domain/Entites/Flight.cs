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

    public void CalculateFlightStatus(DateTime now)
    {
        var diff = DepartureTime - now;

        if (diff.TotalMinutes <= 0 && diff.TotalMinutes > -60)
            Status = FlightStatus.Departed;
        else if (diff.TotalMinutes < 30 && diff.TotalMinutes >= 0)
            Status = FlightStatus.Boarding;
        else if (diff.TotalMinutes <= -60)
            Status = FlightStatus.Landed;
        else
            Status = FlightStatus.Scheduled;
    }
}