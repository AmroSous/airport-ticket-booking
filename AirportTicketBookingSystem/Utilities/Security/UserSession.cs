namespace AirportTicketBookingSystem.Utilities.Security;

public class UserSession
{
    public string? Username { get; private set; }

    public string? Email { get; private set; }

    public void SetUser(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public void ClearSession()
    {
        Username = null;
        Email = null;
    }

    public bool IsUserLoggedIn()
    {
        return !string.IsNullOrEmpty(Username);
    }
}
