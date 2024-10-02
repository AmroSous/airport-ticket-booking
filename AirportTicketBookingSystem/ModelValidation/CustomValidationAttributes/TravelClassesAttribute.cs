using AirportTicketBookingSystem.Models;
using AirportTicketBookingSystem.ModelValidation.ValidationHelper;
using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.ModelValidation.CustomValidationAttributes;

/// <summary>
/// This validation attribute applied on Dictionary<TravelClassEnum, TravelClassDetails>
/// collection to validate TravelClassDetails value properties. 
/// </summary>
public class TravelClassesAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is Dictionary<TravelClassEnum, TravelClassDetails> dictionary)
        {
            List<ValidationResult> results = [];

            foreach (var kvp in dictionary)
            {
                TravelClassDetails details = kvp.Value;

                var result = ModelValidator.Validate(details);

                if (result != ValidationResult.Success || !result.IsValid)
                {
                    results.AddRange(result.Results);
                }
            }

            if (results.Count != 0)
            {
                return new ModelValidationResult("Invalid Flight Travel Classes", results);
            }
        }

        return ValidationResult.Success;
    }
}
