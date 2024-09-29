using AirportTicketBookingSystem.UI;
using AirportTicketBookingSystem.Configuration;
using AirportTicketBookingSystem.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using AirportTicketBookingSystem.Services;
using AirportTicketBookingSystem.Repositories.Interfaces;
using AirportTicketBookingSystem.Services.Interfaces;
using AirportTicketBookingSystem.Factories;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem;

public static class Program
{
    public static void Main()
    {
        #region App Configuration

        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;
                services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

                #region Add DI services

                services.AddTransient<IFlightService, FlightService>();
                services.AddTransient<IPassengerService, PassengerService>();
                services.AddTransient<IBookingService, BookingService>();
                services.AddSingleton<IFlightRepository, FlightRepository>();
                services.AddSingleton<IPassengerRepository, PassengerRepository>();
                services.AddSingleton<IBookingRepository, BookingRepository>();
                services.AddSingleton<FlightFactory>();
                services.AddTransient<IdGenerator>();
                services.AddTransient<AdminUI>();
                services.AddTransient<PassengerUI>();

                #endregion

                // the entry point for our application
                services.AddHostedService<Application>();
            })
            .UseSerilog((context, config) =>
            {
                string defaultLogFile = "../../../logger.log";
                
                // this config logging in console and file
                // and disable Logs related to Host. 

                config/*.WriteTo.Console()*/
                      .WriteTo.File(
                          context.Configuration["AppSettings:LogFilePath"] ?? defaultLogFile, rollingInterval: RollingInterval.Day)
                      .Filter.ByExcluding(logEvent =>
                          logEvent.Properties.TryGetValue("SourceContext", out var source) 
                          && source.ToString().Contains("Microsoft.Hosting.Lifetime"));
            })
            .Build();

        #endregion

        host.Run();
    }
}
