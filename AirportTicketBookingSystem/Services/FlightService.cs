using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.ModelValidation.ValidationHelper;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AirportTicketBookingSystem.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;

    private readonly IBookingService _bookingService;

    public FlightService(IFlightRepository flightRepository, IBookingService bookingService)
    {
        _flightRepository = flightRepository;
        _bookingService = bookingService;
    }

    public OperationResult SaveFlights(List<Flight> flights)
    {
        _ = VerifyListOfFlights(flights, out bool allValid);
        if (!allValid)
        {
            Console.WriteLine();
            return new(false, "Flights are invalid, please verify flights to see the errors.");
        }

        var result = _flightRepository.AddFlights(flights);
        return result;
    }

    public static string VerifyListOfFlights(List<Flight> flights, out bool valid)
    {
        bool allValid = true;
        StringBuilder resultBuilder = new();

        foreach (var flight in flights)
        {
            ModelValidationResult mvr = ModelValidator.Validate(flight);
            if (mvr != ValidationResult.Success && mvr.IsValid == false)
            {
                allValid = false;
                string formattedResult = ValidationResultFormater.Format(mvr);
                resultBuilder.AppendLine(formattedResult);
            }
        }

        valid = allValid;
        return resultBuilder.ToString();
    }

    public IEnumerable<Flight> GetAllFlights()
    {
        return _flightRepository.GetAllFlights();
    }

    public Dictionary<TravelClassEnum, TravelClassDetails> GetAvailableFlightClasses(int flightId)
    {
        var bookingsReserved = _bookingService.GetFlightBookings(flightId);
        var totalClassesInFlight = _flightRepository?.GetFlightById(flightId)?.AvailableTravelClasses ?? [];
        Dictionary<TravelClassEnum, int> availableSeats = new()
        {
            { TravelClassEnum.Economy, totalClassesInFlight[TravelClassEnum.Economy].TotalSeats},
            { TravelClassEnum.Business, totalClassesInFlight[TravelClassEnum.Business].TotalSeats },
            { TravelClassEnum.FirstClass, totalClassesInFlight[TravelClassEnum.FirstClass].TotalSeats }
        };
        foreach (var booking in bookingsReserved) availableSeats[booking.TravelClassEnum]--;

        Dictionary<TravelClassEnum, TravelClassDetails> result = [];
        foreach (var item in availableSeats)
        {
            if (item.Value > 0) result.Add(item.Key, new(totalClassesInFlight[item.Key].Price, item.Value));
        }
        return result;
    }

    public Flight? GetFlight(int flightId)
    {
        return _flightRepository.GetFlightById(flightId);
    }
}
