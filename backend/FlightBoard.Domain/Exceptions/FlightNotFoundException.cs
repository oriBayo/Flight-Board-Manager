using System;

namespace FlightBoard.Domain.Exceptions;

public class FlightNotFoundException : Exception
{
    public FlightNotFoundException(Guid flightId)
        : base($"Flight with ID {flightId} not found.")
    {
    }
}
