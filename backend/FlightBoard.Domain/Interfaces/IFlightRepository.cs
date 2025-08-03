using FlightBoard.Domain.Entities;

namespace FlightBoard.Domain.Interfaces;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetAllFlightsAsync();
    Task CreateFlightAsync(Flight flight);
    Task DeleteFlightAsync(Guid id);
    Task<bool> FlightNumberExistsAsync(string flightNumber);
    Task<IEnumerable<Flight>> SearchFlightsAsync(string? status, string? destination);
    Task UpdateFlightAsync(Flight flight);
}