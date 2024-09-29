using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;

namespace AirportTicketBookingSystem.Repositories.Interfaces;

public interface IPassengerRepository
{
    IEnumerable<Passenger> GetAllPassengers();

    Passenger? FindPassengerByUsername(string username);

    OperationResult AddPassenger(Passenger passenger);

    Task SaveDataAsync();
}
