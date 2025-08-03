using FlightBoard.Application.DTOs;
using Microsoft.AspNetCore.SignalR;

public class FlightNotifierService : IFlightNotifierService
{
    private readonly IHubContext<FlightBoardHub> _hubContext;

    public FlightNotifierService(IHubContext<FlightBoardHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyFlightCreatedAsync(FlightDto flight)
    {
        return _hubContext.Clients.All.SendAsync("FlightCreated", flight);
    }

    public Task NotifyFlightDeletedAsync(Guid flightId)
    {
        return _hubContext.Clients.All.SendAsync("FlightDeleted", flightId);
    }

    public Task NotifyFlightStatusUpdatedAsync(FlightDto flight)
    {
        return _hubContext.Clients.All.SendAsync("FlightUpdated", flight);
    }
}
