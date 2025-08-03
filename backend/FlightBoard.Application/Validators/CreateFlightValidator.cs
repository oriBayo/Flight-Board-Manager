using FlightBoard.Application.DTOs;
using FlightBoard.Domain.Interfaces;
using FluentValidation;

namespace FlightBoard.Application.Validators;

public class CreateFlightValidator : AbstractValidator<CreateFlightDto>
{
    public CreateFlightValidator(IFlightRepository flightRepository)
    {
        RuleFor(x => x.FlightNumber)
        .NotEmpty().WithMessage("Flight number is required.")
        .MustAsync(async (flightNumber, cancellation) => !await flightRepository.FlightNumberExistsAsync(flightNumber)
        ).WithMessage("Flight number must be unique."); ;
        RuleFor(x => x.Destination).NotEmpty().WithMessage("Destination is required.");
        RuleFor(x => x.Gate).NotEmpty().WithMessage("Gate is required.");
        RuleFor(x => x.DepartureTime).NotEmpty().WithMessage("Departure time is required.")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Departure time must be in the future.");
    }
}
