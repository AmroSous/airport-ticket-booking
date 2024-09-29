using AirportTicketBookingSystem.CustomExceptions;
using static AirportTicketBookingSystem.Utilities.ConsolePrinter;

namespace AirportTicketBookingSystem.Utilities;

public static class ConsoleReader
{
    public static string Read()
    {
        string input = Console.ReadLine() ?? "";
        CheckAbortCommand(input);
        return input;
    }

    private static void CheckAbortCommand(string input)
    {
        if (input.Equals("--abort")) throw new AbortProcessException();
    }

    public static int ReadInt()
    {
        string input = Read();
        bool isValid = int.TryParse(input, out int value);
        return isValid ? value : throw new InvalidCastException($"cannot parse {input} to int.");
    }

    public static int ReadIntInRange(int min, int max)
    {
        int choice;
        while (true)
        {
            try
            {
                Print("Enter your input: ");
                choice = ReadInt();
                if (choice >= min && choice <= max) return choice;
            }
            catch (InvalidCastException) { }
        }
    }

    public static T PromptUser<T>(string message)
    {
        string input;
        T value;
        while (true)
        {
            try
            {
                Print(message, ConsoleColor.Cyan);
                input = Read();
                value = (T)Convert.ChangeType(input, typeof(T));
                return value;
            }
            catch (Exception ex) when (ex is not AbortProcessException) { }
        }
    }

    public static void Pause(string message = "")
    {
        Print(message, ConsoleColor.DarkBlue);
        Read();
    }

    public static bool PromptConfirm(string message)
    {
        Print(message, ConsoleColor.DarkGreen);
        string input = Read();
        if (string.IsNullOrEmpty(input)) return false;
        if (input.Equals("y", StringComparison.OrdinalIgnoreCase)) return true;
        return false; 
    }
}
