namespace AirportTicketBookingSystem.Utilities;

public static class ConsolePrinter
{
    public static void Print(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public static void PrintLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Print($"{message}{Environment.NewLine}", color);
    }
}
