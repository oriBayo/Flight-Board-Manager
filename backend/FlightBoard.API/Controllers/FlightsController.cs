using FlightBoard.Application.DTOs;
using FlightBoard.Application.Interfaces;
using FlightBoard.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flights = await _flightService.GetAllFlightsAsync();
            return Ok(flights);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] CreateFlightDto flightDto)
        {
            try
            {
                var createdFlight = await _flightService.CreateFlightAsync(flightDto);

                return CreatedAtAction(nameof(GetAll), new { id = createdFlight.Id }, createdFlight);
            }
            catch (InvalidFlightException ex)
            {
                return BadRequest(new { Errors = ex.Errors });
            }
        }
    }
}
