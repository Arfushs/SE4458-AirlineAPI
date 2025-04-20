using AirlineAPI.DTOs;
using AirlineAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirlineAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost("buy")]
    public async Task<IActionResult> BuyTicket([FromBody] BuyTicketDto dto)
    {
        var result = await _ticketService.BuyTicketAsync(dto);
        if (result == "Sold out")
            return BadRequest(new { message = result });

        return Ok(new { message = "Ticket purchased successfully", seat = result });
    }

    [HttpPut("checkin")]
    public async Task<IActionResult> CheckIn([FromBody] CheckInDto dto)
    {
        var result = await _ticketService.CheckInAsync(dto);
        if (result == "Passenger not found")
            return NotFound(new { message = result });

        return Ok(new { message = result });
    }

    [HttpGet("passengers")]
    public async Task<IActionResult> GetPassengerList(
        [FromQuery] string flightNumber,
        [FromQuery] DateTime date)
    {
        var dto = new PassengerListQueryDto
        {
            FlightNumber = flightNumber,
            Date = date
        };

        var result = await _ticketService.GetPassengerListAsync(dto);
        return Ok(result);
    }
}