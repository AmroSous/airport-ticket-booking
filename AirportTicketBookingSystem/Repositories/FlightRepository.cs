using AirportTicketBookingSystem.Configuration;
using AirportTicketBookingSystem.Factories;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AirportTicketBookingSystem.Repositories;

public class FlightRepository : AbstractRepository, IFlightRepository
{
    private readonly ILogger<FlightRepository> _logger;

    protected override string Filename { get; } = "flights.json";

    protected List<Flight> _flights = [];

    private readonly FlightFactory _flightFactory;

    public FlightRepository(IOptions<AppSettings> options, ILogger<FlightRepository> logger, FlightFactory flightFactory) 
        : base("Data/Flights", options, logger)
    {
        _logger = logger;
        _flightFactory = flightFactory;
        LoadData();
    }

    protected override void LoadData()
    {
        _flights = ReadData<List<Flight>>() ?? [];
        foreach (var flight in _flights)
        {
            _flightFactory.SetIdUsed(flight.Id);
        }
    }

    public IEnumerable<Flight> GetAllFlights()
    {
        return _flights.AsEnumerable();
    }

    public OperationResult AddFlights(List<Flight> flights)
    {
        foreach (var flight in flights)
        {
            if (_flights.Any(f => f.Id == flight.Id))
                return new(false, $"flight ID is used: {flight.Id}");
        }
        _flights.AddRange(flights);
        Dirty = true;
        return new(true, "Flights added successfuly.");
    }

    public async Task SaveDataAsync()
    {
        await SaveDataAsync(_flights);
    }
}
