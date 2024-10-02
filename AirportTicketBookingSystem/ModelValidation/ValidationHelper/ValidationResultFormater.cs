using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AirportTicketBookingSystem.ModelValidation.ValidationHelper;

public static class ValidationResultFormater
{
    public static int TAB_SIZE { get; } = 4; 

    /// <summary>
    /// This method takes a validation result and generate a string in 
    /// a well formatted hiararchy style of error messages.
    /// ValidationResult may be another composite result sub-class
    /// like ModelValidationResult,
    /// so it ensures that all details are read recursivly.
    /// </summary>
    /// <param name="validationResult"></param>
    /// <param name="indentLevel"></param>
    /// <returns></returns>
    public static string Format(ValidationResult validationResult, int indentLevel = 0)
    {
        bool isSuccess = validationResult == ValidationResult.Success;
        ModelValidationResult? modelValidationResult = null; 

        if (validationResult is ModelValidationResult mvr)
        {
            modelValidationResult = mvr;
            if (modelValidationResult.IsValid) isSuccess = true;
        }

        if (isSuccess) return "";

        var sb = new StringBuilder();

        string indent = new(' ', indentLevel * TAB_SIZE);

        sb.Append($"{indent}{validationResult.ErrorMessage}");

        if (modelValidationResult is not null && modelValidationResult.Results.Any())
        {
            sb.AppendLine(" {");
            foreach (var result in modelValidationResult.Results)
            {
                string formatted = Format(result, indentLevel + 1);
                if (!string.IsNullOrEmpty(formatted)) sb.AppendLine(formatted);
            }
            sb.Append($"{indent}}}");
        }

        return sb.ToString();
    }
}
