using AirlineAPI.Data;
using AirlineAPI.DTOs;
using AirlineAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AirlineAPI.Services;

public class FlightService : IFlightService
{
    private readonly AppDbContext _context;

    public FlightService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> AddFlightAsync(FlightDto dto)
    {
        var flightNumber = $"FL-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";

        var flight = new Flight
        {
            FlightNumber = flightNumber,
            AirportFrom = dto.AirportFrom,
            AirportTo = dto.AirportTo,
            DateFrom = dto.DateFrom,
            DateTo = dto.DateTo,
            Duration = dto.Duration,
            Capacity = dto.Capacity
        };

        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();

        return $"Flight added: {flightNumber}";
    }

    public async Task<List<FlightDto>> QueryFlightsAsync(QueryFlightDto dto)
    {
        var query = await _context.Flights
            .Where(f =>
                f.AirportFrom == dto.AirportFrom &&
                f.AirportTo == dto.AirportTo &&
                f.DateFrom.Date >= dto.DateFrom.Date &&
                f.DateTo.Date <= dto.DateTo.Date &&
                f.Capacity >= dto.NumberOfPeople)
            .ToListAsync();

        return query.Select(f => new FlightDto
        {
            AirportFrom = f.AirportFrom,
            AirportTo = f.AirportTo,
            DateFrom = f.DateFrom,
            DateTo = f.DateTo,
            Duration = f.Duration,
            Capacity = f.Capacity
        }).ToList();
    }
}