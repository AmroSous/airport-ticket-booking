namespace AirportTicketBookingSystem.Configuration;

/// <summary>
/// This class is used to inject configuration 
/// from appsettings.json into classes 
/// </summary>
public class AppSettings
{
    public string? CSVUploadsDirectory { get; set; }

    public string? DataStorageDirectory { get; set; }

    public string? LogFilePath { get; set; }

    public AdminPrevillages? adminPrevillages { get; set; }

    public class AdminPrevillages
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

}
