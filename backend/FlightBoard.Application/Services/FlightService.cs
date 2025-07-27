using AutoMapper;
using FlightBoard.Application.DTOs;
using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Exceptions;
using FlightBoard.Domain.Interfaces;
using FluentValidation;

namespace FlightBoard.Application.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateFlightDto> _validator;

    public FlightService(IFlightRepository repository, IMapper mapper, IValidator<CreateFlightDto> validator)
    {
        _repository = repository;
        _validator = validator;
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<IEnumerable<FlightDto>> GetAllFlightsAsync()
    {
        var flights = await _repository.GetAllFlightsAsync();
        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }
    public Task<FlightDto> CreateFlightAsync(CreateFlightDto flightDto)
    {
        var validationResult = _validator.Validate(flightDto);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => new ValidationError
            {
                PropertyName = e.PropertyName,
                ErrorMessage = e.ErrorMessage
            });

            throw new InvalidFlightException(errors);
        }
        Flight flight = _mapper.Map<Flight>(flightDto);
        flight.Id = Guid.NewGuid();
        flight.CalculateFlightStatus(DateTime.UtcNow);
        _repository.AddFlightAsync(flight);

        return Task.FromResult(_mapper.Map<FlightDto>(flight));
    }
    public Task DeleteFlightAsync(Guid id)
    {
        _repository.DeleteFlightAsync(id);
        return Task.CompletedTask;
    }
    public async Task<IEnumerable<FlightDto>> SearchFlightsAsync(string? status, string? destination)
    {
        IEnumerable<Flight> flights = await _repository.SearchFlightsAsync(status, destination);
        return _mapper.Map<IEnumerable<FlightDto>>(flights);
    }

}
