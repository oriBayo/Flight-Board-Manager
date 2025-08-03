using FlightBoard.Application.DTOs;
using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FlightBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IFlightNotifierService _notifierService;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightService flightService, IFlightNotifierService flightNotifier, ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _notifierService = flightNotifier;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all current flights with their calculated status.
        /// </summary>
        /// <returns>A list of flight DTOs</returns>
        /// <response code="200">Returns the list of flights</response>
        [HttpGet]
        public async Task<IActionResult> GetAllFlights()
        {
            _logger.LogInformation("Getting all flights");
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        /// <summary>
        /// Creates a new flight with its calculated status and notifies subscribers.
        /// </summary>
        /// <param name="createFlightDto">The flight details to be created</param>
        /// <returns>The created flight object</returns>
        /// <response code="201">Returns the newly created flight</response>
        /// <response code="400">If the flight data is invalid</response>
        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto createFlightDto)
        {
            _logger.LogInformation("Creating flight with flight number: {FlightNumber}", createFlightDto.FlightNumber);
            try
            {
                var createdFlight = await _flightService.CreateFlightAsync(createFlightDto);
                await _notifierService.NotifyFlightCreatedAsync(createdFlight);
                return CreatedAtAction(nameof(GetAllFlights), null, createdFlight);
            }
            catch (InvalidFlightException ex)
            {
                _logger.LogWarning("Failed to create flight: {Errors}", ex.Errors);
                return BadRequest(new { ex.Errors });
            }
        }
        /// <summary>
        /// Deletes a flight by its ID and notifies subscribers.
        /// </summary>
        /// <param name="id">The ID of the flight to delete</param>
        /// <returns>No content on success</returns>
        /// <response code="204">Flight deleted successfully</response>
        /// <response code="404">Flight with given ID was not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight([FromRoute] Guid id)
        {
            _logger.LogInformation("Deleting flight with ID: {FlightId}", id);
            try
            {
                await _flightService.DeleteFlightAsync(id);
                await _notifierService.NotifyFlightDeletedAsync(id);
                return NoContent();
            }
            catch (FlightNotFoundException ex)
            {
                _logger.LogWarning("Flight not found: {Message}", ex.Message);
                return NotFound(new { ex.Message });
            }
        }

        /// <summary>
        /// Searches for flights by status and/or destination.
        /// </summary>
        /// <param name="status">The status of the flights to search for (e.g., Scheduled, Boarding)</param>
        /// <param name="destination">The destination of the flights to search for</param>
        /// <returns>A list of flights matching the search criteria</returns>
        /// <response code="200">Returns the list of matching flights</response>
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<FlightDto>>> SearchFlights([FromQuery] string? status, string? destination)
        {
            _logger.LogInformation("Searching flights with status='{Status}', destination='{Destination}'", status, destination);

            var flightsResult = await _flightService.SearchFlightsAsync(status, destination);
            return Ok(flightsResult);
        }
    }
}
