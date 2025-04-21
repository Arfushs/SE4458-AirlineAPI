using AirlineAPI.Data;
using AirlineAPI.DTOs;
using AirlineAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> BuyTicketAsync(BuyTicketDto dto)
    {
        var flight = await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightNumber == dto.FlightNumber && f.DateFrom.Date == dto.Date.Date);

        if (flight == null)
            return "Flight not found";

        var soldCount = await _context.Tickets.CountAsync(t => t.FlightId == flight.Id);
        if (soldCount >= flight.Capacity)
            return "Sold out";

        var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.Name == dto.PassengerName);
        if (passenger == null)
        {
            passenger = new Passenger { Name = dto.PassengerName };
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();
        }

        var seat = soldCount + 1;

        var ticket = new Ticket
        {
            TicketNumber = $"T-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}",
            SeatNumber = seat,
            CheckedIn = false,
            FlightId = flight.Id,
            PassengerId = passenger.Id
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return $"Ticket purchased. Seat: {seat}";
    }

    public async Task<string> CheckInAsync(CheckInDto dto)
    {
        var flight = await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightNumber == dto.FlightNumber && f.DateFrom.Date == dto.Date.Date);

        if (flight == null)
            return "Flight not found";

        var passenger = await _context.Passengers.FirstOrDefaultAsync(p => p.Name == dto.PassengerName);
        if (passenger == null)
            return "Passenger not found";

        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.FlightId == flight.Id && t.PassengerId == passenger.Id);

        if (ticket == null)
            return "Ticket not found";

        ticket.CheckedIn = true;
        await _context.SaveChangesAsync();

        return $"Checked in. Seat: {ticket.SeatNumber}";
    }

    public async Task<List<string>> GetPassengerListAsync(PassengerListQueryDto dto)
    {
        var flight = await _context.Flights
            .FirstOrDefaultAsync(f => f.FlightNumber == dto.FlightNumber && f.DateFrom.Date == dto.Date.Date);

        if (flight == null)
            return new List<string>();

        var tickets = await _context.Tickets
            .Include(t => t.Passenger)
            .Where(t => t.FlightId == flight.Id)
            .ToListAsync();

        return tickets
            .Select(t => $"{t.Passenger.Name} - Seat {t.SeatNumber} - CheckedIn: {t.CheckedIn}")
            .ToList();
    }
}
