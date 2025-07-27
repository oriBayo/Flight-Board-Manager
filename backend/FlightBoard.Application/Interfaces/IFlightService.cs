
using FlightBoard.Application.DTOs;

namespace FlightBoard.Application.Interfaces;

public interface IFlightService
{
    Task<IEnumerable<FlightDto>> GetAllFlightsAsync();
    Task<FlightDto> CreateFlightAsync(CreateFlightDto flightDto);
    Task DeleteFlightAsync(Guid id);
    Task<IEnumerable<FlightDto>> SearchFlightsAsync(string? status, string? destination);
}
