namespace AirlineAPI.DTOs;

public class CheckInDto
{
    public string FlightNumber { get; set; } = "";
    public DateTime Date { get; set; }
    public string PassengerName { get; set; } = "";
}