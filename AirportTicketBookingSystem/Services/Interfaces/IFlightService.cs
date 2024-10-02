using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services.Interfaces;

public interface IFlightService
{
    OperationResult SaveFlights(List<Flight> flights);

    IEnumerable<Flight> GetAllFlights();

    Dictionary<TravelClassEnum, TravelClassDetails> GetAvailableFlightClasses(int flightId);

    Flight? GetFlight(int flightId);
}
