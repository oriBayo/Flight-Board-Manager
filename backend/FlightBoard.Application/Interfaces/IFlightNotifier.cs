using FlightBoard.Application.DTOs;

public interface IFlightNotifierService
{
    Task NotifyFlightStatusUpdatedAsync(FlightDto flight);
    Task NotifyFlightCreatedAsync(FlightDto flight);
    Task NotifyFlightDeletedAsync(Guid flightId);
}