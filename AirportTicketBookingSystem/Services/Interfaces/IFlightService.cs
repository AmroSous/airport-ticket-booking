using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services.Interfaces;

public interface IFlightService
{
    OperationResult SaveFlights(List<Flight> flights);
}
