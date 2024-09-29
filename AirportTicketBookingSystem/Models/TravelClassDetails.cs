using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.Models;

public class TravelClassDetails(double price, int seats)
{
    [Display(Name = "Travel class price")]
    [Required(ErrorMessage = "{0} is required")]
    [Range(0.0, double.MaxValue, ErrorMessage = "{0} must be non-negative.")]
    public double Price { get; init; } = price;

    [Display(Name = "Travel class seats")]
    [Required(ErrorMessage = "{0} is required")]
    [Range(0, int.MaxValue, ErrorMessage = "{0} must be non-negative.")]
    public int TotalSeats { get; init; } = seats;

    public override string ToString()
    {
        return $$"""{ "Price": {{Price}}, "Total Seats": {{TotalSeats}} }""";
    }
}
