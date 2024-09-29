using AirportTicketBookingSystem.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using System.Text.Json;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem.Repositories;

public abstract class AbstractRepository
{
    protected readonly string _storagepath;

    protected abstract string Filename { get; }

    protected string Filepath { get => Path.Combine(_storagepath, Filename); }

    private readonly ILogger<AbstractRepository> _logger;

    protected bool Dirty { get; set; } = false;

    public AbstractRepository(string subpath, IOptions<AppSettings> options, ILogger<AbstractRepository> logger)
    {
        _logger = logger;
        AppSettings settings = options.Value;
        _storagepath = Path.Combine(settings.DataStorageDirectory ?? "./", subpath);
        EnsureDirectoryExists();
    }

    protected void EnsureDirectoryExists()
    {
        if (!Directory.Exists(_storagepath))
        {
            Directory.CreateDirectory(_storagepath);
            _logger.LogInformation("Storage Directory created {path}", _storagepath);
        }
    }

    protected abstract void LoadData();

    private JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    protected async Task SaveDataAsync<T>(T data)
    {
        if (!Dirty) return;

        try
        {
            await SerializationHelper.SerializeToFileAsync(data, Filepath, _options);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error serializing data to {Filepath}: {Message}", Filepath, ex.Message);
        }
    }

    protected T? ReadData<T>()
    {
        try
        {
            return SerializationHelper.DeserializeFromFile<T>(Filepath);
        }
        catch (FileNotFoundException)
        {
            return default;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error deserializing data from {Filepath}: {Message}", Filepath, ex.Message);
            throw;
        }
    }
}
