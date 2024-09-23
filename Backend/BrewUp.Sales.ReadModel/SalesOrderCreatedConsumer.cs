using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Domain;
using BrewUp.Sales.Shared;
using BrewUp.Shared.Events;
using BrewUp.Shared.Models;

namespace BrewUp.Sales.ReadModel;

public class SalesOrderCreatedConsumer
{
    private readonly ServiceBusProcessor _processor;

    public SalesOrderCreatedConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        var serviceBusClient = new ServiceBusClient(azureServiceBusConfiguration.ConnectionString);
        
        _processor = serviceBusClient.CreateProcessor(
            topicName: "salesordercreated",
            subscriptionName: "sales", new ServiceBusProcessorOptions
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
            var message = await BrewUpSerializer.DeserializeAsync<SalesOrderCreated>(args.Message.Body.ToString());
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
    
    private Task ConsumeAsync(SalesOrderCreated message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        // Persist data on database ...
        Console.WriteLine($"Received message: {message}");
        
        return Task.CompletedTask;
    }
    
    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)=> Task.CompletedTask;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _processor.StartProcessingAsync(cancellationToken);
    }
}