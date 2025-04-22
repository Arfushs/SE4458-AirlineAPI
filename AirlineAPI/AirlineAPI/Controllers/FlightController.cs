using AirlineAPI.DTOs;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddFlight([FromBody] FlightDto dto)
    {
        var result = await _flightService.AddFlightAsync(dto);
        return Ok(new { message = result });
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryFlights([FromQuery] QueryFlightDto dto, [FromQuery] int page = 1)
    {
        var result = await _flightService.QueryFlightsAsync(dto);

        var pageSize = 10;
        var paged = result
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(paged);
    }



}