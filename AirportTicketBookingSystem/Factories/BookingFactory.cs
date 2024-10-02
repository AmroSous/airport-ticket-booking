using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Factories;

public static class BookingFactory
{
    public static Booking CrateBooking(Passenger passenger, Flight flight, TravelClassEnum travelClassEnum)
    {
        DateTime bookingDate = DateTime.Now;
        string id = flight.Id + "-" + passenger.Username + "-" + bookingDate.Ticks;
        return new(id, passenger, flight, travelClassEnum, bookingDate);
    }
}
