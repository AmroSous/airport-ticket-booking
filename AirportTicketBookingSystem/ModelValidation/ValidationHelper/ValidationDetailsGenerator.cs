using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AirportTicketBookingSystem.ModelValidation.ValidationHelper;

public static class ValidationDetailsGenerator
{
    /// <summary>
    /// This generic method returns all validation constraints details about 
    /// the specified type.
    /// </summary>
    /// <typeparam name="T">type to print its details.</typeparam>
    /// <returns>list of PropertyValidationDetails</returns>
    public static List<PropertyValidationDetails> GenerateValidationDetails<T>()
    {
        var validationDetails = new List<PropertyValidationDetails>();

        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            string displayName = property.GetCustomAttribute<DisplayAttribute>()?.Name ?? property.Name;

            PropertyValidationDetails propertyDetails = new(displayName, property.PropertyType.Name);

            var attributes = property.GetCustomAttributes<ValidationAttribute>();
            propertyDetails.Constraints.AddRange(
                attributes
                .Select(attr => attr.ErrorMessage?.Replace("{0}", displayName) ?? attr.GetType().Name));

            validationDetails.Add(propertyDetails);
        }

        return validationDetails;
    }
}
