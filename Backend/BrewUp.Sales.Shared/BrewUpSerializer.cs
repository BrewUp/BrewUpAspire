using Newtonsoft.Json;

namespace BrewUp.Sales.Shared;

public class BrewUpSerializer
{
    public static Task<T> DeserializeAsync<T>(string serializedData, CancellationToken cancellationToken = default) where T : class
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult<T>(JsonConvert.DeserializeObject<T>(serializedData)!);
    }

    public static Task<string> SerializeAsync<T>(T data, CancellationToken cancellationToken = default) where T : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        return Task.FromResult(JsonConvert.SerializeObject((object)data));
    }
}