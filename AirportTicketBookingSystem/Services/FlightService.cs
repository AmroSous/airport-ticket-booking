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

    public FlightService(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
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
}
