using AirportTicketBookingSystem.CustomExceptions;
using AirportTicketBookingSystem.Services.Interfaces;
using static AirportTicketBookingSystem.Utilities.ConsolePrinter;
using static AirportTicketBookingSystem.Utilities.ConsoleReader;
using Microsoft.Extensions.Logging;
using static AirportTicketBookingSystem.UI.DisplayHelpers.AdminDisplayHelper;
using AirportTicketBookingSystem.Factories;
using AirportTicketBookingSystem.Configuration;
using Microsoft.Extensions.Options;
using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.ModelValidation.ValidationHelper;

namespace AirportTicketBookingSystem.UI;

public class AdminUI : IUserInterface
{
    private readonly ILogger<AdminUI> _logger;

    private readonly FlightFactory _flightFactory;

    private readonly AppSettings _settings;

    private readonly IFlightService _flightService;

    public AdminUI(IFlightService flightService, IOptions<AppSettings> options, 
        ILogger<AdminUI> logger, FlightFactory flightFactory)
    {
        _logger = logger;
        _flightFactory = flightFactory;
        _settings = options.Value;
        _flightService = flightService;
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
        ShowAdminScreenHeader();
        ShowAdminMenu();
        int choice = ReadIntInRange(1, 4);
        switch (choice)
        {
            case 1: 
                Console.Clear();
                UploadNewFlightsProcedure();
                break;
            case 2: 
                Console.Clear();
                ShowFlightsValidationConstraintsProcedure();
                break;
            case 3:
                Console.Clear();
                ShowBookingsProcedure();
                break;
            default: 
                LogoutProcedure();
                break;
        }
    }

    private void ShowBookingsProcedure()
    {
        PrintLine("Not implemented yet.", ConsoleColor.Red);
        Pause("continue..");
    }

    private void ShowFlightsValidationConstraintsProcedure()
    {
        var details = ValidationDetailsGenerator.GenerateValidationDetails<Flight>();
        details.ForEach(detail => PrintLine(detail.ToString(), ConsoleColor.DarkGreen));
        Pause("continue..");
    }

    private void UploadNewFlightsProcedure()
    {
        var path = _settings.CSVUploadsDirectory ?? throw new InvalidOperationException("Uploads directory path is null.");
        bool confirmed = PromptConfirm($"Make sure you put the upload file into {path}. continue ? [y/n]: ");
        if (!confirmed) return;

        var flights = UploadFlightFiles(path);
        UploadedFlightsMenu(flights);
    }

    private List<Flight> UploadFlightFiles(string path)
    {
        var files = Directory.GetFiles(path);
        List<Flight> flights = [];
        foreach (var file in files)
        {
            flights.AddRange(Utilities.CSVFlightReader.ReadFlightsFromCSV(file, _flightFactory));
        }
        return flights;
    }

    private void UploadedFlightsMenu(List<Flight> uploadedFlights)
    {
        while (true)
        {
            ShowUploadFlightsMenu();
            int choice = ReadIntInRange(1, 4);
            switch (choice)
            {
                case 1:
                    ShowUploadedFlights(uploadedFlights);
                    break;
                case 2:
                    VerifyUploadedFlights(uploadedFlights);
                    break;
                case 3:
                    SaveUploadedFlights(uploadedFlights);
                    break;
                default:
                    return;
            }
        }
    }

    private void SaveUploadedFlights(List<Flight> uploadedFlights)
    {
        var result = _flightService.SaveFlights(uploadedFlights);
        PrintLine(result.Message, result.Success ? ConsoleColor.Green : ConsoleColor.Red);
        Pause("continue..");
    }

    private void VerifyUploadedFlights(List<Flight> uploadedFlights)
    {
        string formattedResult = FlightService.VerifyListOfFlights(uploadedFlights, out bool allValid);
        if (allValid) PrintLine("All flights are valid, you can save them.", ConsoleColor.Green);
        else PrintLine(formattedResult, ConsoleColor.Red);
        Pause("continue..");
    }

    private void LogoutProcedure()
    {
        PrintLine("Logged out", ConsoleColor.Blue);
        throw new ExitProgramException();
    }
}
