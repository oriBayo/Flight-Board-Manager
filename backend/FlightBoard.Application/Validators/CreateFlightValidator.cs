using FlightBoard.Application.DTOs;
using FluentValidation;

namespace FlightBoard.Application.Validators;

public class CreateFlightValidator : AbstractValidator<CreateFlightDto>
{
    public CreateFlightValidator()
    {
        RuleFor(x => x.FlightNumber).NotEmpty().WithMessage("Flight number is required.");
        RuleFor(x => x.Destination).NotEmpty().WithMessage("Destination is required.");
        RuleFor(x => x.Gate).NotEmpty().WithMessage("Gate is required.");
        RuleFor(x => x.DepartureTime).NotEmpty().WithMessage("Departure time is required.")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("Departure time must be in the future.");
    }
}
