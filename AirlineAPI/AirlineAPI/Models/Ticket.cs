namespace AirlineAPI.Models;

public class Ticket
{
    public int Id { get; set; }
    public string TicketNumber { get; set; } = "";
    public int SeatNumber { get; set; }
    public bool CheckedIn { get; set; }

    public int FlightId { get; set; }
    public Flight Flight { get; set; }

    public int PassengerId { get; set; }
    public Passenger Passenger { get; set; }
}