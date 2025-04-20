using AirlineAPI.DTOs;

namespace AirlineAPI.Services;

public interface IFlightService
{
    Task<string> AddFlightAsync(FlightDto dto);
    Task<List<FlightDto>> QueryFlightsAsync(QueryFlightDto dto);

}