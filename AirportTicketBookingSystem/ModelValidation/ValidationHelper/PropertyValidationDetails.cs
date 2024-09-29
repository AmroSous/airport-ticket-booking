using System.Text;

namespace AirportTicketBookingSystem.ModelValidation.ValidationHelper;

/// <summary>
/// This class holds validation constraints details.
/// </summary>
public class PropertyValidationDetails(string propertyName, string propertyType)
{
    public string PropertyName { get; init; } = propertyName;

    public string PropertyType { get; init; } = propertyType;

    public List<string> Constraints { get; init; } = [];

    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"{PropertyName}:");
        sb.AppendLine($"  > Type: {PropertyType}");
        sb.AppendLine($"  > Constraints:");

        foreach (var detail in Constraints)
        {
            sb.AppendLine($"      - {detail}");
        }

        return sb.ToString();
    }
}
