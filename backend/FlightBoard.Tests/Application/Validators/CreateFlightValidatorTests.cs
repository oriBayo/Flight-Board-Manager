using System.Threading.Tasks;
using FlightBoard.Application.DTOs;
using FlightBoard.Application.Validators;
using FlightBoard.Domain.Interfaces;
using FluentValidation.TestHelper;
using Moq;

namespace FlightBoard.Tests.Application.Validators;

public class CreateFlightValidatorTests
{
    private readonly Mock<IFlightRepository> _repositoryMock;
    private readonly CreateFlightValidator _validator;
    public CreateFlightValidatorTests()
    {
        _repositoryMock = new Mock<IFlightRepository>();
        _validator = new CreateFlightValidator(_repositoryMock.Object);
    }

    [Fact]
    public async Task Validator_ShouldFail_WhenFlightNumberIsEmpty()
    {
        // Given
        var validator = new CreateFlightValidator(_repositoryMock.Object);

        var dto = new CreateFlightDto
        {
            FlightNumber = "",
            Destination = "Paris",
            DepartureTime = DateTime.UtcNow.AddHours(1),
            Gate = "A2"
        };
        _repositoryMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
        // When
        var result = await validator.TestValidateAsync(dto);

        // Then
        result.ShouldHaveValidationErrorFor(f => f.FlightNumber).WithErrorMessage("Flight number is required.");

    }

    [Fact]
    public async Task Validator_ShouldFail_WhenFlightNumberIsExists()
    {
        // Given
        var dto = new CreateFlightDto
        {
            FlightNumber = "1",
            Destination = "Paris",
            DepartureTime = DateTime.UtcNow.AddHours(1),
            Gate = "A2"
        };

        _repositoryMock.Setup(r => r.FlightNumberExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

        // When
        var result = await _validator.TestValidateAsync(dto);

        // Then
        result.ShouldHaveValidationErrorFor(f => f.FlightNumber).WithErrorMessage("Flight number must be unique.");
    }

    [Fact]
    public async Task Validator_ShouldFail_WhenDepartureTimeIsOnPast()
    {
        // Given
        var dto = new CreateFlightDto
        {
            FlightNumber = "1",
            Destination = "Paris",
            DepartureTime = DateTime.Now.AddHours(-10),
            Gate = "A2"
        };

        _repositoryMock.Setup(r => r.FlightNumberExistsAsync(dto.FlightNumber)).ReturnsAsync(false);

        // When
        var result = await _validator.TestValidateAsync(dto);

        // Then
        result.ShouldHaveValidationErrorFor(f => f.DepartureTime).WithErrorMessage("Departure time must be in the future.");

    }
}
