using AirportTicketBookingSystem.Factories;
using AirportTicketBookingSystem.Models;

namespace AirportTicketBookingSystem.Utilities;

// TODO: this implementation is bad because of hard coded fields names
// may be refactored in future using reflection 

public static class CSVFlightReader
{
    public static List<Flight> ReadFlightsFromCSV(string filepath, FlightFactory flightFactory)
    {
        var rows = CSVFileReader.ReadDataRows(filepath);
        List<Flight> flights = [];

        foreach (var row in rows)
        {
            var travelClasses = ReadTravelClassesFromRow(row);

            flights.Add(
                flightFactory.CreateNewFlight(
                    row["DepartureCountry"],
                    row["DestinationCountry"],
                    DateTime.Parse(row["DepartureDate"]),
                    row["DepartureAirport"],
                    row["ArrivalAirport"],
                    travelClasses));
        }

        return flights;
    }

    private static Dictionary<TravelClassEnum, TravelClassDetails> ReadTravelClassesFromRow(Dictionary<string, string> row)
    {
        Dictionary<TravelClassEnum, TravelClassDetails> travelClasses = [];

        if (row.TryGetValue("EconomySeats", out string? economySeats)
                && row.TryGetValue("EconomyPrice", out string? economyPrice))
        {
            if (double.TryParse(economyPrice, out var price)
                && int.TryParse(economySeats, out var seats))
            {
                travelClasses.Add(TravelClassEnum.Economy, new TravelClassDetails(
                    price, seats
                ));
            }
        }

        if (row.TryGetValue("BusinessSeats", out string? businessSeats)
            && row.TryGetValue("BusinessPrice", out string? businessPrice))
        {
            if (double.TryParse(businessPrice, out var price)
                && int.TryParse(businessSeats, out var seats))
            {
                travelClasses.Add(TravelClassEnum.Business, new TravelClassDetails(
                    price, seats
                ));
            }
        }

        if (row.TryGetValue("FirstClassSeats", out string? firstClassSeats)
            && row.TryGetValue("FirstClassPrice", out string? firstClassPrice))
        {
            if (double.TryParse(firstClassPrice, out var price)
                && int.TryParse(firstClassSeats, out var seats))
            {
                travelClasses.Add(TravelClassEnum.FirstClass, new TravelClassDetails(
                    price, seats
                ));
            }
        }

        return travelClasses;
    }
}
