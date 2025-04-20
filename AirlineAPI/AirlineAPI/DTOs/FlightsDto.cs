namespace AirlineAPI.DTOs;

public class FlightDto
{
    public string AirportFrom { get; set; } = "";
    public string AirportTo { get; set; } = "";
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }
}