using AirlineAPI.DTOs;

namespace AirlineAPI.Services;

public interface ITicketService
{
    Task<string> BuyTicketAsync(BuyTicketDto dto);
    Task<string> CheckInAsync(CheckInDto dto);
    Task<List<string>> GetPassengerListAsync(PassengerListQueryDto dto);
}