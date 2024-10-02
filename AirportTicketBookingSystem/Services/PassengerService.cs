using AirportTicketBookingSystem.Configuration;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services.Interfaces;
using AirportTicketBookingSystem.Utilities.Security;
using Microsoft.Extensions.Options;

namespace AirportTicketBookingSystem.Services;

public class PassengerService : IPassengerService
{
    private readonly IPassengerRepository _passengerRepository;

    private readonly AppSettings _settings;

    public PassengerService(IPassengerRepository passengerRepository, IOptions<AppSettings> options)
    {
        _passengerRepository = passengerRepository;
        _settings = options.Value;
    }

    public OperationResult CreateAccount(string username, string email, string password)
    {
        string hashedPassword = PasswordHasher.HashPassword(password);
        return _passengerRepository.AddPassenger(new(username, email, hashedPassword));
    }

    public bool IsValidPassenger(string username, string password)
    {
        Passenger? passenger = _passengerRepository.FindPassengerByUsername(username);
        if (passenger == null) return false;
        if (PasswordHasher.VerifyPassword(password, passenger.HashedPassword)) return true;
        return false;
    }

    public bool IsValidAdmin(string username, string password)
    {
        return (_settings?.adminPrevillages?.Username?.Equals(username) ?? false)
            && (_settings?.adminPrevillages?.Password?.Equals(password) ?? false);
    }

    public Passenger? GetPassenger(string username)
    {
        return _passengerRepository.FindPassengerByUsername(username);
    }
}
