using AirlineAPI.DTOs;

namespace AirlineAPI.Services;

public class TicketService : ITicketService
{
    private static readonly List<(string FlightNumber, DateTime Date, string PassengerName, int SeatNumber, bool CheckedIn)> Tickets = new();
    private static int _seatCounter = 1;

    public Task<string> BuyTicketAsync(BuyTicketDto dto)
    {
        // Mock kapasite kontrolü: Aynı uçuşta 5 koltuk sınırı olsun
        int currentPassengerCount = Tickets
            .Count(t => t.FlightNumber == dto.FlightNumber && t.Date.Date == dto.Date.Date);

        if (currentPassengerCount >= 5)
            return Task.FromResult("Sold out");

        var seat = _seatCounter++;
        Tickets.Add((dto.FlightNumber, dto.Date.Date, dto.PassengerName, seat, false));
        return Task.FromResult($"Ticket purchased. Seat: {seat}");
    }

    public Task<string> CheckInAsync(CheckInDto dto)
    {
        var index = Tickets.FindIndex(t =>
            t.FlightNumber == dto.FlightNumber &&
            t.Date.Date == dto.Date.Date &&
            t.PassengerName == dto.PassengerName);

        if (index == -1)
            return Task.FromResult("Passenger not found");

        var ticket = Tickets[index];
        Tickets[index] = (ticket.FlightNumber, ticket.Date, ticket.PassengerName, ticket.SeatNumber, true);
        return Task.FromResult($"Checked in. Seat: {ticket.SeatNumber}");
    }

    public Task<List<string>> GetPassengerListAsync(PassengerListQueryDto dto)
    {
        var list = Tickets
            .Where(t => t.FlightNumber == dto.FlightNumber && t.Date.Date == dto.Date.Date)
            .Select(t => $"{t.PassengerName} - Seat {t.SeatNumber} - CheckedIn: {t.CheckedIn}")
            .ToList();

        return Task.FromResult(list);
    }
}