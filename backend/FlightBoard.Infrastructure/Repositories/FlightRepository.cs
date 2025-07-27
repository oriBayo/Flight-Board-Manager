using System;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Exceptions;
using FlightBoard.Domain.Interfaces;
using FlightBoard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBoard.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly ApplicationDbContext _context;
    public FlightRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddFlightAsync(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFlightAsync(Guid id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new FlightNotFoundException(id);
        }
    }

    public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task<IEnumerable<Flight>> SearchFlightsAsync(string? status, string? destination)
    {
        var query = _context.Flights.AsQueryable();

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(f => f.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(destination))
        {
            query = query.Where(f => f.Destination.Contains(destination));
        }

        return await query.ToListAsync();
    }
}

