using AirlineAPI.DTOs;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FlightController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddFlight([FromBody] FlightDto dto)
    {
        var result = await _flightService.AddFlightAsync(dto);
        return Ok(new { message = result });
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryFlights(
        [FromQuery] string airportFrom,
        [FromQuery] string airportTo,
        [FromQuery] DateTime dateFrom,
        [FromQuery] DateTime dateTo,
        [FromQuery] int numberOfPeople,
        [FromQuery] bool isRoundTrip)
    {
        var result = await _flightService.QueryFlightsAsync(new QueryFlightDto
        {
            AirportFrom = airportFrom,
            AirportTo = airportTo,
            DateFrom = dateFrom,
            DateTo = dateTo,
            NumberOfPeople = numberOfPeople,
            IsRoundTrip = isRoundTrip
        });

        return Ok(result);
    }
}