namespace AirlineAPI.Models;

public class Passenger
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}