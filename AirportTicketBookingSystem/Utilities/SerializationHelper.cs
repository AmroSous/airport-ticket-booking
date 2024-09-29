using System.Text.Json;
using Newtonsoft.Json;

namespace AirportTicketBookingSystem.Utilities;

public static class SerializationHelper
{
    public static async Task SerializeToFileAsync<T>(T data, string filepath, JsonSerializerOptions options)
    {
        using FileStream createStream = File.Create(filepath);
        await System.Text.Json.JsonSerializer.SerializeAsync(createStream, data, options);
    }

    public static T? DeserializeFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException($"file not found: {filePath}");

        var json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<T>(json);
    }
}
