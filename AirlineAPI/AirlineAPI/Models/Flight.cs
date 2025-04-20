namespace AirlineAPI.Models;

public class Flight
{
    public int Id { get; set; }
    public string FlightNumber { get; set; } = "";
    public string AirportFrom { get; set; } = "";
    public string AirportTo { get; set; } = "";
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int Duration { get; set; }
    public int Capacity { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}