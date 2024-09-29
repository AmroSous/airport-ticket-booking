using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Repositories.Interfaces;

public interface IFlightRepository
{
    IEnumerable<Flight> GetAllFlights();

    OperationResult AddFlights(List<Flight> flights);

    Task SaveDataAsync();
}
