using System.ComponentModel.DataAnnotations;

namespace AirportTicketBookingSystem.ModelValidation.ValidationHelper;

/// <summary>
/// This is a sub-class of built-in ValidationResult class
/// it inherits its functionality and add the ability to store 
/// more than one ValidationResult in IEnumerable collection. 
/// 
/// we can use this class to return the validation result of 
/// complex properties like Dictionaries that have objects that 
/// their properties must be validated.
/// </summary>
public class ModelValidationResult : ValidationResult
{
    public IEnumerable<ValidationResult> Results { get; private set; }

    public bool IsValid { get; private set; }

    public ModelValidationResult(string errorMessage, bool isValid = false)
        : base(errorMessage)
    {
        Results = [];
        IsValid = isValid;
    }

    public ModelValidationResult(string errorMessage, IEnumerable<ValidationResult> validationResults, bool isValid = false)
        : base(errorMessage)
    {
        Results = validationResults;
        IsValid = isValid;
    }
}
