using AirportTicketBookingSystem.Models;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsolePrinter;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsoleReader;

namespace AirportTicketBookingSystem.UI.DisplayHelpers;

public static class AdminDisplayHelper
{
    public static void ShowAdminMenu()
    {
        PrintLine("""

        ╔═══════════════════════════════════════════╗
        ║                                           ║
        ║   1- Upload new flights.                  ║
        ║   2- Show flights validation constraint.  ║
        ║   3- Show bookings.                       ║
        ║   4- Logout.                              ║                     
        ║                                           ║
        ╚═══════════════════════════════════════════╝

        """, ConsoleColor.Magenta);
    }

    public static void ShowUploadFlightsMenu()
    {
        PrintLine("""

        ╔═══════════════════════════════════════════╗
        ║                                           ║
        ║   1- Show uploaded flights.               ║
        ║   2- Validate Flights.                    ║
        ║   3- Save flights.                        ║
        ║   4- Cancel.                              ║                     
        ║                                           ║
        ╚═══════════════════════════════════════════╝

        """, ConsoleColor.Magenta);
    }

    public static void ShowUploadedFlights(List<Flight> uploadedFlights)
    {
        uploadedFlights.ForEach(flight => PrintLine(flight.ToString(), ConsoleColor.Green));
        Pause("continue..");
    }

    public static void ShowAdminScreenHeader()
    {
        PrintLine("""
            
              ___      _           _                                
             / _ \    | |         (_)                               
            / /_\ \ __| |_ __ ___  _ _ __    _ __   __ _  __ _  ___ 
            |  _  |/ _` | '_ ` _ \| | '_ \  | '_ \ / _` |/ _` |/ _ \
            | | | | (_| | | | | | | | | | | | |_) | (_| | (_| |  __/
            \_| |_/\__,_|_| |_| |_|_|_| |_| | .__/ \__,_|\__, |\___|
                                            | |           __/ |     
                                            |_|          |___/      
            

            """, ConsoleColor.Yellow);
    }

    public static void ShowAllBookings(Dictionary<string, List<Booking>> bookings)
    {
        foreach (var userBookings in bookings)
        {
            PrintLine($"{userBookings.Key}", ConsoleColor.Yellow);
            PrintLine("═════════════════════════════════════════════════════════════", ConsoleColor.DarkRed);

            foreach (var booking in userBookings.Value)
            {
                PrintLine(booking.ToString(), ConsoleColor.Blue);
                PrintLine("──────────────────────────────────────────────────────", ConsoleColor.DarkGray);
            }
        }
    }
}
