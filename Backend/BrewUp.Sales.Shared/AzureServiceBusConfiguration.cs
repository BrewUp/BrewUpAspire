namespace BrewUp.Sales.Domain;

public record AzureServiceBusConfiguration(string ConnectionString, string TopicName, string ClientId, int MaxConcurrentCalls = 1);