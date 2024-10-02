using AirportTicketBookingSystem.CustomExceptions;
using AirportTicketBookingSystem.Services;
using static AirportTicketBookingSystem.UI.DisplayHelpers.PassengerDisplayHelper;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsoleReader;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsolePrinter;
using Microsoft.Extensions.Logging;
using AirportTicketBookingSystem.Services.Interfaces;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Utilities.Security;

namespace AirportTicketBookingSystem.UI;

public class PassengerUI : IUserInterface
{
    private readonly ILogger<PassengerUI> _logger;

    private readonly IFlightService _flightService;

    private readonly IBookingService _bookingService;

    private readonly UserSession _session;

    public PassengerUI(ILogger<PassengerUI> logger, IFlightService flightService, 
        IBookingService bookingService, UserSession session)
    {
        _logger = logger;
        _flightService = flightService;
        _bookingService = bookingService;
        _session = session;
    }

    public void Start()
    {
        if (!_session.IsUserLoggedIn()) throw new InvalidOperationException("Invalid user session.");
        
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
                BookFlightProcedure();
                break;
            case 3:
                Console.Clear();
                ManageBookingsProcedure();
                break;
            default:
                LogoutProcedure();
                break;
        }
    }

    private void BookFlightProcedure()
    {
        int flightId = PromptUser<int>("Enter flight ID: ");
        var classesResult = _flightService.GetAvailableFlightClasses(flightId);
        if (classesResult.Count == 0)
        {
            PrintLine("No seats available for this flight.", ConsoleColor.Red);
        }
        else
        {
            PrintAvailableSeatsDetails(classesResult);
            int choice = -1;
            while (choice < 1 || choice > classesResult.Count)
            {
                choice = PromptUser<int>("Enter travel class number: ");
            }
            var travelClassEnum = classesResult.Skip(choice - 1).First().Key;
            EnsureValidSession();
            var result = _bookingService.BookFlight(flightId, _session.Username, travelClassEnum);
            PrintLine(result.Message, result.Success ? ConsoleColor.Green : ConsoleColor.Red);
        }
        Pause("continue..");
    }

    private void ManageBookingsProcedure()
    {
        EnsureValidSession();
        var result = _bookingService.GetUserBookings(_session.Username);
        PrintUserBookings(result);
        Pause("continue..");
    }

    private void EnsureValidSession()
    {
        if (!_session.IsUserLoggedIn()) 
            throw new InvalidOperationException("Invalid user session");
    }

    private void SearchFlightsProcedure()
    {
        var flights = _flightService.GetAllFlights();
        PrintListOfFlights(flights);
        Pause("continue..");
    }

    private void LogoutProcedure()
    {
        PrintLine("Logged out", ConsoleColor.Blue);
        throw new ExitProgramException();
    }
}
