using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Domain;

namespace BrewUp.Sales.Shared;

public class AzureEventBus
{
    private readonly ServiceBusSender _serviceBusSender;

    public AzureEventBus(AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        ServiceBusClient serviceBusClient = new (azureServiceBusConfiguration.ConnectionString);
        _serviceBusSender = serviceBusClient.CreateSender("salesordercreated");
    }

    public async Task PublishAsync(string message)
    {
        var serviceBusMessage = new ServiceBusMessage(message)
        {
            CorrelationId = Guid.NewGuid().ToString(),
            MessageId = Guid.NewGuid().ToString(),
            ApplicationProperties = { { "EventName", "salesordercreated" } }
        };
        await _serviceBusSender.SendMessageAsync(serviceBusMessage, CancellationToken.None).ConfigureAwait(false);
    }
}