using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsolePrinter;

namespace AirportTicketBookingSystem.UI.DisplayHelpers;

public static class HomeDisplayHelper
{
    public static void ShowWelcomeMessage()
    {
        PrintLine("""
             __     __     ______     __         ______     ______     __    __     ______    
            /\ \  _ \ \   /\  ___\   /\ \       /\  ___\   /\  __ \   /\ "-./  \   /\  ___\   
            \ \ \/ ".\ \  \ \  __\   \ \ \____  \ \ \____  \ \ \/\ \  \ \ \-./\ \  \ \  __\   
             \ \__/".~\_\  \ \_____\  \ \_____\  \ \_____\  \ \_____\  \ \_\ \ \_\  \ \_____\ 
              \/_/   \/_/   \/_____/   \/_____/   \/_____/   \/_____/   \/_/  \/_/   \/_____/ 
                                                                                              
            """, ConsoleColor.DarkYellow);
    }

    public static void ShowDescription()
    {
        PrintLine("""

            This is a our console application for an airport ticket booking system. 
            This application enables passengers to book flight tickets, show, modify his bookings,
            and allows a manager to manage the bookings, add new flights, and verifyng them...


            """, ConsoleColor.Green);
    }

    public static void ShowCommands()
    {
        Print("[NEW] ", ConsoleColor.DarkRed);
        PrintLine("you can use --abort at any moment to abort the current process, and return to previous screen.", ConsoleColor.Blue);
    }

    public static void ShowHome()
    {
        ShowWelcomeMessage();
        ShowDescription();
        ShowCommands();
        Console.WriteLine();
        Console.WriteLine();
        ShowSignInMenu();
    }

    public static void ShowSignInMenu()
    {
        PrintLine("""
            ╔══════════════════════════╗
            ║                          ║
            ║   1- Create account.     ║
            ║   2- Sign in passenger.  ║
            ║   3- Sign in admin.      ║
            ║   4- Exit.               ║                     
            ║                          ║
            ╚══════════════════════════╝

            """, ConsoleColor.Magenta);
    }

    public static void ShowByeMessage()
    {
        PrintLine("""

            Bye..

            """, ConsoleColor.DarkBlue);
    }
}
