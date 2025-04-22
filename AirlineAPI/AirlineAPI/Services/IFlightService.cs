using AirlineAPI.DTOs;

namespace AirlineAPI.Services;

public interface IFlightService
{
    Task<string> AddFlightAsync(FlightDto dto);
    Task<List<AvailableFlightDto>> QueryFlightsAsync(QueryFlightDto dto);
}