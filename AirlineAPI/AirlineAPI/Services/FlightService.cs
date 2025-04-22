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
        Console.WriteLine("[INFO] AddFlightAsync started");

        try
        {
            var flightNumber = $"FL-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";
            Console.WriteLine($"[INFO] Generated Flight Number: {flightNumber}");

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

            Console.WriteLine($"[SUCCESS] Flight saved: {flightNumber}");
            return $"Flight added: {flightNumber}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ERROR] AddFlightAsync failed: {ex.Message}");
            throw; 
        }
    }


    public async Task<List<AvailableFlightDto>> QueryFlightsAsync(QueryFlightDto dto)
    {
        var query = await _context.Flights
            .Where(f =>
                f.AirportFrom == dto.AirportFrom &&
                f.AirportTo == dto.AirportTo &&
                f.DateFrom.Date >= dto.DateFrom.Date &&
                f.DateTo.Date <= dto.DateTo.Date &&
                f.Capacity >= dto.NumberOfPeople)
            .ToListAsync();

        return query.Select(f => new AvailableFlightDto
        {
            FlightNumber = f.FlightNumber,
            Duration = f.Duration
        }).ToList();
    }

}