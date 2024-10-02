using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services.Interfaces;

public interface IBookingService
{
    OperationResult BookFlight(int flightId, string? username, TravelClassEnum travelClassEnum);

    List<Booking> GetUserBookings(string? username);

    List<Booking> GetFlightBookings(int flightId);

    Dictionary<string, List<Booking>> GetAllBookings();
}
