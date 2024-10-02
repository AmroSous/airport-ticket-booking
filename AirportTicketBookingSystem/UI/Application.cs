using static AirportTicketBookingSystem.UI.DisplayHelpers.HomeDisplayHelper;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsoleReader;
using static AirportTicketBookingSystem.Utilities.ConsoleIO.ConsolePrinter;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AirportTicketBookingSystem.CustomExceptions;
using AirportTicketBookingSystem.Services.Interfaces;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Utilities.Security;

namespace AirportTicketBookingSystem.UI;

public class Application : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    private readonly ILogger<Application> _logger;

    private readonly IPassengerService _passengerService;

    private readonly IHostApplicationLifetime _lifetime;

    private readonly IPassengerRepository _passengerRepository;

    private readonly IFlightRepository _flightRepository;

    private readonly IBookingRepository _bookingRepository;

    private readonly UserSession _session;

    public Application(IServiceProvider serviceProvider, 
        ILogger<Application> logger, 
        IPassengerService passengerService,
        IHostApplicationLifetime lifetime,
        IFlightRepository flightRepository,
        IPassengerRepository passengerRepository,
        IBookingRepository bookingRepository,
        UserSession session)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _passengerService = passengerService;
        _lifetime = lifetime;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
        _bookingRepository = bookingRepository;
        _session = session;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        // Register the async callback to ensure data is saved when the app is stopping.
        _lifetime.ApplicationStopping.Register(() =>
        {
            Task.Run(async () => await OnShutdownAsync()).Wait();
        });

        while (true)
        {
            try
            {
                Console.Clear();
                StartHomeProcedure();
            }
            catch (ExitProgramException)
            {
                break;
            }
            catch (AbortProcessException)
            {
            }
            catch (Exception ex)
            {
                _logger.LogError("Exeption was thown: {Message}", ex.Message);
                PrintLine("Unexpected exception thrown.", ConsoleColor.Red);
                Pause("logout..");
                break;
            }
        }
        
        _lifetime.StopApplication();
        return Task.CompletedTask;
    }

    private async Task OnShutdownAsync()
    {
        try
        {
            await _flightRepository.SaveDataAsync();
            _logger.LogInformation("flights data saved successfully.");
            await _passengerRepository.SaveDataAsync();
            _logger.LogInformation("passengers data saved successfully.");
            await _bookingRepository.SaveDataAsync();
            _logger.LogInformation("bookings data saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error saving data during shutdown: {Message}", ex.Message);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        PrintLine("Application stoped.");
        return Task.CompletedTask;
    }

    private IUserInterface GetInterfaceService<T>() where T : IUserInterface
    {
        using var scope = _serviceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    private void StartHomeProcedure()
    {
        ShowHome();
        int choice = ReadIntInRange(1, 4);
        if (choice == 1)
        {
            Console.Clear();
            CreateAccountProcedure();
        }
        else if (choice == 2)
        {
            Console.Clear();
            LoginProcedure();
        }
        else if (choice == 3)
        {
            Console.Clear();
            LoginAdminProcedure();
        }
        else
        {
            ExitProgram();
        }
    }

    private void ExitProgram()
    {
        ShowByeMessage();
        throw new ExitProgramException();
    }

    private void CreateAccountProcedure()
    {
        string username = PromptUser<string>("Username: ");
        string email = PromptUser<string>("Email: ");
        string password = PromptUser<string>("Password: ");
        string confirmPassword = PromptUser<string>("Confirm Password: ");

        if (!password.Equals(confirmPassword))
        {
            Console.WriteLine();
            PrintLine("Password and Confirm Password doesn't match.", ConsoleColor.Red);
        }
        else
        {
            var result = _passengerService.CreateAccount(username, email, password);
            Console.WriteLine();
            PrintLine(result.Message, result.Success ? ConsoleColor.DarkGreen : ConsoleColor.Red);
        }

        Console.WriteLine();
        Pause("Press any key to return home.");
    }

    private void LoginProcedure()
    {
        string username = PromptUser<string>("Username: ");
        string password = PromptUser<string>("Password: ");
        Console.WriteLine();

        bool isValidUser = _passengerService.IsValidPassenger(username, password);

        if (isValidUser)
        {
            IUserInterface userInterface = GetInterfaceService<PassengerUI>();
            _session.SetUser(username, "");
            userInterface.Start();
        } 
        else
        {
            PrintLine("Invalid username or password.", ConsoleColor.Red);
            Console.WriteLine();
            Pause("Press any key to go home..");
        }
    }

    private void LoginAdminProcedure()
    {
        string username = PromptUser<string>("Username: ");
        string password = PromptUser<string>("Password: ");
        Console.WriteLine();

        bool isAdmin = _passengerService.IsValidAdmin(username, password);

        if (isAdmin)
        {
            IUserInterface userInterface = GetInterfaceService<AdminUI>();
            _session.SetUser(username, "");
            userInterface.Start();
        }
        else
        {
            PrintLine("Invalid username or password.", ConsoleColor.Red);
            Console.WriteLine();
            Pause("Press any key to go home..");
        }
    }
}
