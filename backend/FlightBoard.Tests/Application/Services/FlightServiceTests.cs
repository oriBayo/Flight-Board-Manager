using AutoMapper;
using FlightBoard.Application.DTOs;
using FlightBoard.Application.Mappers;
using FlightBoard.Application.Services;
using FlightBoard.Domain.Entities;
using FlightBoard.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace FlightBoard.Tests.Application.Services;

public class FlightServiceTests
{
    private readonly Mock<IFlightRepository> _repositoryMock;
    private readonly Mock<IValidator<CreateFlightDto>> _validatorMock;
    private readonly Mapper _mapper;
    private readonly FlightService _service;

    public FlightServiceTests()
    {
        _repositoryMock = new Mock<IFlightRepository>();
        _validatorMock = new Mock<IValidator<CreateFlightDto>>();
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<FlightMappingProfile>()));
        _service = new FlightService(_repositoryMock.Object, _mapper, _validatorMock.Object);
    }

    [Fact]
    public async Task GetAllFlightsAsync_ShouldReturnFlights()
    {
        // Given
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var mockFlights = new List<Flight>
        {
            new Flight{Id=id1, Gate="A",Destination="LA",FlightNumber="1"},
            new Flight{Id=id2, Gate="B",Destination="DA",FlightNumber="2"},
         };

        _repositoryMock.Setup(r => r.GetAllFlightsAsync()).ReturnsAsync(mockFlights);
        // When
        var result = await _service.GetAllFlightsAsync();

        // Then
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        var resultList = result.ToList();
        Assert.Equal(id1, resultList[0].Id);
        Assert.Equal("LA", resultList[0].Destination);
        Assert.Equal("1", resultList[0].FlightNumber);
        Assert.Equal("A", resultList[0].Gate);

        Assert.Equal(id2, resultList[1].Id);
        Assert.Equal("DA", resultList[1].Destination);
        Assert.Equal("2", resultList[1].FlightNumber);
        Assert.Equal("B", resultList[1].Gate);

    }

    [Fact]
    public async Task CreateFlightAsync_ShouldReturnCreatedFlight()
    {
        // Given
        var dto = new CreateFlightDto
        {
            FlightNumber = "Test",
            Destination = "Test",
            Gate = "Test",
            DepartureTime = DateTime.Now.AddHours(2)
        };

        _repositoryMock.Setup(r => r.CreateFlightAsync(It.IsAny<Flight>())).Returns((Flight f) => Task.FromResult(f));

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CreateFlightDto>(), default)).ReturnsAsync(new ValidationResult());

        // When
        var result = await _service.CreateFlightAsync(dto);

        // Then
        Assert.NotNull(result);
        Assert.Equal(dto.FlightNumber, result.FlightNumber);
    }
    [Fact]
    public async Task DeleteFlightAsync_ShouldCallRepositoryWithCorrectId()
    {
        // Given
        var id = Guid.NewGuid();

        // When
        await _service.DeleteFlightAsync(id);

        // Then
        _repositoryMock.Verify(r => r.DeleteFlightAsync(id), Times.Once);
    }

    [Fact]
    public async Task SearchFlightsAsync_ShouldReturnMappedFlights_WhenFlightsExist()
    {
        // Given
        var status = "OnTime";
        var destination = "London";

        var mockFlights = new List<Flight>
    {
        new Flight
        {
            Id = Guid.NewGuid(),
            FlightNumber = "BA123",
            Destination = "London",
            Gate = "A1",
            DepartureTime = DateTime.Now.AddHours(2)
        },
        new Flight
        {
            Id = Guid.NewGuid(),
            FlightNumber = "BA456",
            Destination = "London",
            Gate = "A2",
            DepartureTime = DateTime.Now.AddHours(3)
        }
    };

        _repositoryMock.Setup(r => r.SearchFlightsAsync(status, destination)).ReturnsAsync(mockFlights);


        // When
        var result = await _service.SearchFlightsAsync(status, destination);

        // Then
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());

        var resultList = result.ToList();
        Assert.Equal("BA123", resultList[0].FlightNumber);
        Assert.Equal("BA456", resultList[1].FlightNumber);
        Assert.All(resultList, flight => Assert.Equal("London", flight.Destination));

        _repositoryMock.Verify(r => r.SearchFlightsAsync(status, destination), Times.Once);
    }

    [Fact]
    public async Task SearchFlightsAsync_ShouldReturnEmptyCollection_WhenNoFlightsFound()
    {
        // Given
        var status = "Boarding";
        var destination = "Paris";
        var emptyFlights = new List<Flight>();

        _repositoryMock.Setup(r => r.SearchFlightsAsync(status, destination))
                     .ReturnsAsync(emptyFlights);

        var flightService = new FlightService(_repositoryMock.Object, _mapper, _validatorMock.Object);

        // When
        var result = await flightService.SearchFlightsAsync(status, destination);

        // Then
        Assert.NotNull(result);
        Assert.Empty(result);

        _repositoryMock.Verify(r => r.SearchFlightsAsync(status, destination), Times.Once);
    }

    [Fact]
    public async Task SearchFlightsAsync_ShouldHandleNullParameters()
    {
        // Given
        string? status = null;
        string? destination = null;

        var mockFlights = new List<Flight>
    {
        new Flight
        {
            Id = Guid.NewGuid(),
            FlightNumber = "LH789",
            Destination = "Berlin",
            Gate = "B3",
            DepartureTime = DateTime.Now.AddHours(1)
        }
    };

        _repositoryMock.Setup(r => r.SearchFlightsAsync(null, null))
                     .ReturnsAsync(mockFlights);

        var flightService = new FlightService(_repositoryMock.Object, _mapper, _validatorMock.Object);

        // When
        var result = await flightService.SearchFlightsAsync(status, destination);

        // Then
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("LH789", result.First().FlightNumber);

        _repositoryMock.Verify(r => r.SearchFlightsAsync(null, null), Times.Once);
    }

    [Theory]
    [InlineData("Scheduled", null)]
    [InlineData(null, "Tokyo")]
    [InlineData("Boarding", "New York")]
    public async Task SearchFlightsAsync_ShouldCallRepositoryWithCorrectParameters(string? status, string? destination)
    {
        // Given
        var mockFlights = new List<Flight>();

        _repositoryMock.Setup(r => r.SearchFlightsAsync(It.IsAny<string>(), It.IsAny<string>()))
                     .ReturnsAsync(mockFlights);

        var flightService = new FlightService(_repositoryMock.Object, _mapper, _validatorMock.Object);

        // When
        await flightService.SearchFlightsAsync(status, destination);

        // Then
        _repositoryMock.Verify(r => r.SearchFlightsAsync(status, destination), Times.Once);
    }
}