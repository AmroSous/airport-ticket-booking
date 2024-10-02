using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Services.Interfaces;

public interface IPassengerService
{
    bool IsValidPassenger(string username, string password);

    bool IsValidAdmin(string username, string password);

    OperationResult CreateAccount(string username, string email, string password);

    Passenger? GetPassenger(string username);
}
