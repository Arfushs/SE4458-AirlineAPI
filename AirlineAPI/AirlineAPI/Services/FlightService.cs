using AirlineAPI.DTOs;

namespace AirlineAPI.Services;

public class FlightService : IFlightService
{
    private static readonly List<(int Id, FlightDto Flight)> Flights = new();
    private static int _flightIdCounter = 1;

    public Task<string> AddFlightAsync(FlightDto dto)
    {
        Flights.Add((_flightIdCounter++, dto));
        return Task.FromResult("Flight added.");
    }

    public Task<List<FlightDto>> QueryFlightsAsync(QueryFlightDto dto)
    {
        var result = Flights
            .Where(f =>
                f.Flight.AirportFrom == dto.AirportFrom &&
                f.Flight.AirportTo == dto.AirportTo &&
                f.Flight.DateFrom.Date >= dto.DateFrom.Date &&
                f.Flight.DateTo.Date <= dto.DateTo.Date &&
                f.Flight.Capacity >= dto.NumberOfPeople
            )
            .Select(f => f.Flight)
            .ToList();

        return Task.FromResult(result);
    }
    
}