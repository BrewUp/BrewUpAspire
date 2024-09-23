namespace BrewUp.Infrastructures.Azure;

public class AzureServiceBusSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TopicName { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
}