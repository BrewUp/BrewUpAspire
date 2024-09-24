using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Shared;

namespace BrewUp.Sales.Domain;

public abstract class CommandConsumerBase<T> where T : class
{
    protected readonly ServiceBusProcessor Processor;
    protected readonly AzureEventBus AzureEventBus;

    protected CommandConsumerBase(AzureServiceBusConfiguration azureServiceBusConfiguration,
        AzureEventBus eventBus)
    {
        AzureEventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        var serviceBusClient = new ServiceBusClient(azureServiceBusConfiguration.ConnectionString);
        
        Processor = serviceBusClient.CreateProcessor(
            topicName: typeof(T).Name,
            subscriptionName: "", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = azureServiceBusConfiguration.MaxConcurrentCalls
            });
    }
}