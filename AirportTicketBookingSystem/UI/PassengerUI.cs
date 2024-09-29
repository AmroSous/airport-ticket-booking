using AirportTicketBookingSystem.CustomExceptions;
using AirportTicketBookingSystem.Services;
using static AirportTicketBookingSystem.UI.DisplayHelpers.PassengerDisplayHelper;
using static AirportTicketBookingSystem.Utilities.ConsoleReader;
using static AirportTicketBookingSystem.Utilities.ConsolePrinter;
using Microsoft.Extensions.Logging;

namespace AirportTicketBookingSystem.UI;

public class PassengerUI : IUserInterface
{
    private readonly ILogger<PassengerUI> _logger; 

    public PassengerUI(ILogger<PassengerUI> logger)
    {
        _logger = logger;
    }

    public void Start()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                StartHomeProcedure();
            }
            catch (ExitProgramException)
            {
                throw;
            }
            catch (AbortProcessException)
            {
            }
            catch (Exception ex)
            {
                _logger.LogError("Exeption was thown: {Message}", ex.Message);
                throw new ExitProgramException();
            }
        }
    }

    private void StartHomeProcedure()
    {
        ShowPassengerScreenHeader();
        ShowPassengerMenu();
        int choice = ReadIntInRange(1, 4);
        switch (choice)
        {
            case 1:
                Console.Clear();
                SearchFlightsProcedure();
                break;
            case 2:
                Console.Clear();
                ManageBookingsProcedure();
                break;
            default:
                LogoutProcedure();
                break;
        }
    }

    private void ManageBookingsProcedure()
    {
        PrintLine("Not implemented yet.", ConsoleColor.Red);
        Pause("continue..");
    }

    private void SearchFlightsProcedure()
    {
        PrintLine("Not implemented yet.", ConsoleColor.Red);
        Pause("continue..");
    }

    private void LogoutProcedure()
    {
        PrintLine("Logged out", ConsoleColor.Blue);
        throw new ExitProgramException();
    }
}
