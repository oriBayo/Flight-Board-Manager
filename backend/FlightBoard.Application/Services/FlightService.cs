using AutoMapper;
using FlightBoard.Application.DTOs;
using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Exceptions;
using FlightBoard.Domain.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FlightBoard.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateFlightDto> _validator;
    private readonly ILogger<FlightService> _logger;

    public FlightService(IFlightRepository repository, IMapper mapper, IValidator<CreateFlightDto> validator, ILogger<FlightService> logger)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        _logger.LogInformation("Fetching all flights from repository...");
        var flights = await _repository.GetAllFlightsAsync();

        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }
    public async Task<FlightDto> CreateFlightAsync(CreateFlightDto flightDto)
    {

        var validationResult = await _validator.ValidateAsync(flightDto);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for flight: {FlightNumber}", flightDto.FlightNumber);
            var errors = validationResult.Errors.Select(e => new ValidationError
            {
                PropertyName = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            });

            throw new InvalidFlightException(errors);
        }
        Flight flight = _mapper.Map<Flight>(flightDto);
        flight.Id = Guid.NewGuid();
        flight.CalculateFlightStatus(DateTime.Now);
        await _repository.CreateFlightAsync(flight);
        return _mapper.Map<FlightDto>(flight);
    }
    public async Task DeleteFlightAsync(Guid id)
    {
        await _repository.DeleteFlightAsync(id);
        _logger.LogInformation("Flight with ID: {FlightId} deleted successfully", id);
    }
    public async Task<IEnumerable<FlightDto>> SearchFlightsAsync(string? status, string? destination)
    {
        _logger.LogInformation("Searching flights with status: {Status} and destination: {Destination}", status, destination);

        IEnumerable<Flight> flights = await _repository.SearchFlightsAsync(status, destination);

        _logger.LogInformation("Found {Count} matching flights", flights.Count());

        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }

}
