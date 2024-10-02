using Newtonsoft.Json;

namespace AirportTicketBookingSystem.Models;

public class Passenger
{
    [JsonConstructor]
    public Passenger(string username, string email, string hashedPassword)
    {
        Username = username;
        Email = email;
        HashedPassword = hashedPassword;
    }

    public string Username { get; init; }

    public string Email { get; init; }

    public string HashedPassword { get; init; }
}
