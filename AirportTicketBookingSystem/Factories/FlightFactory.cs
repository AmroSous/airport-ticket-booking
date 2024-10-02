using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilities.Security;

namespace AirportTicketBookingSystem.Factories;

/// <summary>
/// This is a factory class for creating Flight instances. 
/// This class controls the Id for flights, because there are several 
/// methods of creating Flights into our system. 
/// </summary>
public sealed class FlightFactory(IdGenerator idGenerator)
{
    private readonly IdGenerator _idGenerator = idGenerator;

    /// <summary>
    /// generating new Id for created flight. 
    /// </summary>
    public Flight CreateNewFlight(string departureCountry, string destinationCountry, DateTime departureDate,
                                  string departureAirport, string arrivalAirport,
                                  Dictionary<TravelClassEnum, TravelClassDetails> travelClasses)
    {
        int newId = _idGenerator.Next;
        return new Flight(newId, departureCountry, destinationCountry, departureDate,
            departureAirport, arrivalAirport, travelClasses);
    }

    /// <summary>
    /// Ensures that Id is unique.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public Flight CreateExistingFlight(int id, string departureCountry, string destinationCountry, DateTime departureDate,
                                     string departureAirport, string arrivalAirport,
                                     Dictionary<TravelClassEnum, TravelClassDetails> travelClasses)
    {
        if (_idGenerator.IsUsed(id))
        {
            throw new InvalidOperationException("Fail creating a flight, ID must be unique."); 
        }

        return new Flight(id, departureCountry, destinationCountry, departureDate,
            departureAirport, arrivalAirport, travelClasses);
    }

    public void SetIdUsed(int id)
    {
        _idGenerator.SetUsed(id);
    }

    public bool IsIdUsed(int id)
    {
        return _idGenerator.IsUsed(id);
    }
}
