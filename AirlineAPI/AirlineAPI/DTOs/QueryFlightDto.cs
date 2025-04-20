namespace AirlineAPI.DTOs;

public class QueryFlightDto
{
    public string AirportFrom { get; set; } = "";
    public string AirportTo { get; set; } = "";
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int NumberOfPeople { get; set; }
    public bool IsRoundTrip { get; set; }
}