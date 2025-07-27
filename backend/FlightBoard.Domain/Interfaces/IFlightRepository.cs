using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Interfaces;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task AddFlightAsync(Flight flight);
    Task DeleteFlightAsync(Guid id);
    Task<IEnumerable<Flight>> SearchFlightsAsync(string? status, string? destination);
}