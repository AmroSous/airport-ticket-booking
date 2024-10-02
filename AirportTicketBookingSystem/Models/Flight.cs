using AirportTicketBookingSystem.ModelValidation.CustomValidationAttributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models;

public class Flight
{
    [JsonConstructor] 
    public Flight(int id, string departureCountry, string destinationCountry, DateTime departureDate,
               string departureAirport, string arrivalAirport,
               Dictionary<TravelClassEnum, TravelClassDetails> travelClasses)
    {
        Id = id;
        DepartureCountry = departureCountry;
        DestinationCountry = destinationCountry;
        DepartureDate = departureDate;
        DepartureAirport = departureAirport;
        ArrivalAirport = arrivalAirport;
        AvailableTravelClasses = travelClasses;
    }

    [Display(Name = "Flight ID")]
    [Range(1, int.MaxValue, ErrorMessage = "{0} must be positive integer.")]
    [Required(ErrorMessage = "{0} is required.")]
    public int Id { get; init; }

    [Display(Name = "Departure Country")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "{0} name must contain letters only.")]
    [Required(ErrorMessage = "{0} is required.")]
    public string DepartureCountry { get; init; }

    [Display(Name = "Destination Country")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30.")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "{0} name must contain letters only.")]
    [Required(ErrorMessage = "{0} is required.")]
    public string DestinationCountry { get; init; }

    [Display(Name = "Departure Date")]
    [DataType(DataType.Date, ErrorMessage = "{0} must be of Date type.")]
    [Required(ErrorMessage = "{0} is required.")]
    public DateTime DepartureDate { get; init; }

    [Display(Name = "Departure Airport")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30.")]
    [Required(ErrorMessage = "{0} is required.")]
    public string DepartureAirport { get; init; }

    [Display(Name = "Arrival Airport")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "{0} name must be between 2 and 30.")]
    [Required(ErrorMessage = "{0} is required.")]
    public string ArrivalAirport { get; init; }

    [Display(Name = "Flight Travel Classes")]
    [Required(ErrorMessage = "{0} is required.")]
    [TravelClasses]
    public Dictionary<TravelClassEnum, TravelClassDetails> AvailableTravelClasses { get; init; }

    public override string ToString()
    {
        return $$"""
            ID: {{Id}},
            Path: {{DepartureCountry}} ───> {{DestinationCountry}},
            Airports: {{DepartureAirport}} ───> {{ArrivalAirport}},
            Date: {{DepartureDate}},
            Available classes: 
                - Economy: {{AvailableTravelClasses.GetValueOrDefault(TravelClassEnum.Economy)}},
                - Business: {{AvailableTravelClasses.GetValueOrDefault(TravelClassEnum.Business)}},
                - First Class: {{AvailableTravelClasses.GetValueOrDefault(TravelClassEnum.FirstClass)}}
            """;
    }
}
