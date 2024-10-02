using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.ModelValidation.ValidationHelper;

public static class ModelValidator
{
    /// <summary>
    /// Generic utility method to apply model validation on specific 
    /// type instance.
    /// </summary>
    /// <typeparam name="T">Type of instance to be validated.</typeparam>
    /// <param name="instance">Instance to be validated.</param>
    /// <returns>ModelValidationResult</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static ModelValidationResult Validate<T>(T instance) where T : class
    {
        if (instance == null) throw new InvalidOperationException("Cannot validate null instance.");

        ValidationContext validationContext = new(instance);
        List<ValidationResult> validationResults = [];

        bool isValid = Validator.TryValidateObject(instance, validationContext, validationResults, validateAllProperties: true);

        return new ModelValidationResult($"Invalid Instance of type {instance.GetType().Name}", validationResults, isValid);
    }
}
