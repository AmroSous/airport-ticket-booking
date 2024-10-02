namespace AirportTicketBookingSystem.CustomExceptions;

/// <summary>
/// used to return to the main method to exit program
/// </summary>
internal class ExitProgramException : Exception
{
    public ExitProgramException()
    {
    }

    public ExitProgramException(string? message) : base(message)
    {
    }
}
