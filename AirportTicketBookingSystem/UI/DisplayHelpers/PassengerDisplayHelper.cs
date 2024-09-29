using static AirportTicketBookingSystem.Utilities.ConsolePrinter;

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
        ║   2- Manage bookings.                     ║
        ║   3- Logout.                              ║
        ║                                           ║
        ╚═══════════════════════════════════════════╝

        """, ConsoleColor.Magenta);
    }
}
