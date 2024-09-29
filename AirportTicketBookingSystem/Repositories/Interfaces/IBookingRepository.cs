using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Repositories.Interfaces;

public interface IBookingRepository
{
    Task SaveDataAsync();

    Dictionary<string, List<Booking>> GetAllBookings();

    OperationResult AddBooking(Booking booking);

    OperationResult DeleteBooking(string bookingId);
}
