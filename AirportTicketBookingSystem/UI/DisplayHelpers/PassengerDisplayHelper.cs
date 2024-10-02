using AirportTicketBookingSystem.Models;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsolePrinter;

namespace AirportTicketBookingSystem.UI.DisplayHelpers;

public static class PassengerDisplayHelper
{
    public static void ShowPassengerScreenHeader()
    {
        PrintLine("""
            
            ______                                                                 
            | ___ \                                                                
            | |_/ /_ _ ___ ___  ___ _ __   __ _  ___ _ __   _ __   __ _  __ _  ___ 
            |  __/ _` / __/ __|/ _ \ '_ \ / _` |/ _ \ '__| | '_ \ / _` |/ _` |/ _ \
            | | | (_| \__ \__ \  __/ | | | (_| |  __/ |    | |_) | (_| | (_| |  __/
            \_|  \__,_|___/___/\___|_| |_|\__, |\___|_|    | .__/ \__,_|\__, |\___|
                                           __/ |           | |           __/ |     
                                          |___/            |_|          |___/      
             

            """, ConsoleColor.Yellow);
    }

    public static void ShowPassengerMenu()
    {
        PrintLine("""

        ╔═══════════════════════════════════════════╗
        ║                                           ║
        ║   1- Search flights.                      ║
        ║   2- Book a flight.                       ║
        ║   3- Manage bookings.                     ║
        ║   4- Logout.                              ║
        ║                                           ║
        ╚═══════════════════════════════════════════╝

        """, ConsoleColor.Magenta);
    }

    public static void PrintListOfFlights(IEnumerable<Flight> flights)
    {
        PrintLine("──────────────────────────────────────────────────────", ConsoleColor.DarkGray);
        foreach (var flight in flights)
        {
            PrintLine(flight.ToString(), ConsoleColor.Yellow);
            PrintLine("──────────────────────────────────────────────────────", ConsoleColor.DarkGray);
        }
    }

    public static void PrintAvailableSeatsDetails(Dictionary<TravelClassEnum, TravelClassDetails> classes)
    {
        foreach (var item in classes)
        {
            PrintLine($"{item.Key} => {item.Value}", ConsoleColor.Blue);
        }
    }

    public static void PrintUserBookings(List<Booking> bookings)
    {
        if (bookings.Count == 0)
        {
            PrintLine("No available bookings.", ConsoleColor.Red);
        }
        else
        {
            int index = 1;
            PrintLine("──────────────────────────────────────────────────────", ConsoleColor.DarkGray);
            foreach (var item in bookings)
            {
                PrintLine($"{index++}: {item}", ConsoleColor.Blue);
                PrintLine("──────────────────────────────────────────────────────", ConsoleColor.DarkGray);
            }
        }
    }
}
