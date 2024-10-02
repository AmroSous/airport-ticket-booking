using AirportTicketBookingSystem.Factories;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services.Interfaces;

namespace AirportTicketBookingSystem.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    private readonly IPassengerService _passengerService;

    private readonly IFlightRepository _flightRepository;

    public BookingService(IBookingRepository bookingRepository, IPassengerService passengerService, IFlightRepository flightRepository)
    {
        _bookingRepository = bookingRepository;
        _passengerService = passengerService;
        _flightRepository = flightRepository;
    }

    public OperationResult BookFlight(int flightId, string? username, TravelClassEnum travelClassEnum)
    {
        if (username == null) return new(false, "Invalid username.");
        var passenger = _passengerService.GetPassenger(username);
        if (passenger == null) return new(false, "Invalid user.");

        var flight = _flightRepository.GetFlightById(flightId);
        if (flight == null) return new(false, "Invalid flight ID.");

        var result = _bookingRepository.AddBooking(BookingFactory.CrateBooking(passenger, flight, travelClassEnum));
        return result;
    }

    public Dictionary<string, List<Booking>> GetAllBookings()
    {
        return _bookingRepository.GetAllBookings();
    }

    public List<Booking> GetFlightBookings(int flightId)
    {
        List<Booking> result = [];
        foreach (var item in _bookingRepository.GetAllBookings())
        {
            result.AddRange(item.Value.Where(booking => booking.Flight.Id == flightId));
        }
        return result;
    }

    public List<Booking> GetUserBookings(string? username)
    {
        if (string.IsNullOrEmpty(username)) return [];
        return _bookingRepository.GetAllBookings()[username];
    }
}
