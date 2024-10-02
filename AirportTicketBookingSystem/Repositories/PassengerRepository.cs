using AirportTicketBookingSystem.Configuration;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AirportTicketBookingSystem.Repositories;

public class PassengerRepository : AbstractRepository, IPassengerRepository
{
    private readonly ILogger<PassengerRepository> _logger;

    protected override string Filename { get; } = "passengers.json";

    protected List<Passenger> _passengers = [];

    public PassengerRepository(IOptions<AppSettings> options, ILogger<PassengerRepository> logger) 
        : base("Data/Passengers", options, logger)
    {
        _logger = logger;
        LoadData();
    }

    protected override void LoadData()
    {
        _passengers = ReadData<List<Passenger>>() ?? [];
    }

    public IEnumerable<Passenger> GetAllPassengers()
    {
        return _passengers.AsEnumerable();
    }

    public Passenger? FindPassengerByUsername(string username)
    {
        return GetAllPassengers().SingleOrDefault(x => x.Username.Equals(username));
    }

    public OperationResult AddPassenger(Passenger passenger)
    {
        if (GetAllPassengers().Any(x => x.Username == passenger.Username))
        {
            return new(false, "Username used.");
        }

        Dirty = true;
        _passengers.Add(passenger);
        return new(true, "Passenger added successfully");
    }

    public async Task SaveDataAsync()
    {
        await SaveDataAsync(_passengers);
    }
}
