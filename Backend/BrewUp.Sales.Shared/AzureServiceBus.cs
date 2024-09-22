using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Domain;

namespace BrewUp.Sales.Shared;

public class AzureServiceBus
{
    private readonly ServiceBusSender _serviceBusSender;

    public AzureServiceBus(AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        ServiceBusClient serviceBusClient = new (azureServiceBusConfiguration.ConnectionString);
        _serviceBusSender = serviceBusClient.CreateSender("createsalesorder");
    }

    public async Task SendAsync(string message)
    {
        var command = await BrewUpSerializer.SerializeAsync(message);
        var serviceBusMessage = new ServiceBusMessage(command)
        {
            CorrelationId = Guid.NewGuid().ToString(),
            MessageId = Guid.NewGuid().ToString(),
            ApplicationProperties = { { "CommandName", "createsalesorder" } }
        };
        await _serviceBusSender.SendMessageAsync(serviceBusMessage, CancellationToken.None).ConfigureAwait(false);
    }
    
    public async Task PublishAsync(string message)
    {
        var @event = await BrewUpSerializer.SerializeAsync(message);
        await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(@event), CancellationToken.None);
    }
}