using AirportTicketBookingSystem.Configuration;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AirportTicketBookingSystem.Repositories;

public class BookingRepository : AbstractRepository, IBookingRepository
{
    private readonly ILogger<BookingRepository> _logger;

    protected override string Filename { get; } = "bookings.json";

    private Dictionary<string, List<Booking>> _bookings = [];

    public BookingRepository(IOptions<AppSettings> options, ILogger<BookingRepository> logger) 
        : base("Data/Bookings", options, logger)
    {
        _logger = logger;
        LoadData();
    }

    protected override void LoadData()
    {
        _bookings = ReadData<Dictionary<string, List<Booking>>>() ?? [];
    }

    public async Task SaveDataAsync()
    {
        await SaveDataAsync(_bookings);
    }

    public Dictionary<string, List<Booking>> GetAllBookings()
    {
        return _bookings;
    }

    public OperationResult AddBooking(Booking booking)
    {
        _bookings[booking.Passenger.Username].Add(booking);
        Dirty = true;
        return new(true, "Flight booked successfully.");
    }

    public OperationResult DeleteBooking(string bookingId)
    {
        string username = bookingId.Split('-')[1];

        int index = _bookings[username].FindIndex(booking => booking.Id == bookingId);

        if (index == -1) return new(false, $"cannot delete booking, booking id not exist: {bookingId}");

        _bookings[username].RemoveAt(index);
        return new(true, "booking removed successfully.");
    }
}
