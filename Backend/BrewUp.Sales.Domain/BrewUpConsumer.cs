using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Shared;
using BrewUp.Shared.Commands;

namespace BrewUp.Sales.Domain;

public class BrewUpConsumer
{
    private readonly ServiceBusProcessor _processor;
    private readonly BrewUpSerializer _serializer;

    public BrewUpConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        var serviceBusClient = new ServiceBusClient(azureServiceBusConfiguration.ConnectionString);
        
        _processor = serviceBusClient.CreateProcessor(
            topicName: "createsalesorder",
            subscriptionName: "", new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false,
                MaxConcurrentCalls = azureServiceBusConfiguration.MaxConcurrentCalls
            });
        _processor.ProcessMessageAsync += AzureMessageHandler;
        _processor.ProcessErrorAsync += ProcessErrorAsync;
    }
    
    private async Task AzureMessageHandler(ProcessMessageEventArgs args)
    {
        try
        {
            var message = await BrewUpSerializer.DeserializeAsync<CreateSalesOrder>(args.Message.Body.ToString());
            await ConsumeAsync(message, args.CancellationToken);

            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            if (args.Message.DeliveryCount > 3)
                await args.DeadLetterMessageAsync(args.Message).ConfigureAwait(false);
            else
                await args.AbandonMessageAsync(args.Message).ConfigureAwait(false);
        }
    }

    private async Task ConsumeAsync(CreateSalesOrder message, CancellationToken cancellationToken)
    {
        
    }
    
    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)=> Task.CompletedTask;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _processor.StartProcessingAsync(cancellationToken);
    }
}