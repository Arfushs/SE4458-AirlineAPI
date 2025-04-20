namespace AirlineAPI.DTOs;

public class BuyTicketDto
{
    public string FlightNumber { get; set; } = "";
    public DateTime Date { get; set; }
    public string PassengerName { get; set; } = "";
}