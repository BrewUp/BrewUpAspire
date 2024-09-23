using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Shared;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Events;

namespace BrewUp.Sales.Domain;

public class CreateSalesOrderConsumer
{
    private readonly ServiceBusProcessor _processor;
    private readonly AzureEventBus _eventBus;

    public CreateSalesOrderConsumer(AzureServiceBusConfiguration azureServiceBusConfiguration,
        AzureEventBus eventBus)
    {
        _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
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
        cancellationToken.ThrowIfCancellationRequested();
        
        SalesOrderCreated @event = new(message.SalesOrderId, message.SalesOrderNumber,
            message.CustomerId, message.CustomerName,
            message.TotalAmount, message.Currency,
            message.Rows);
        
        var serializedMessage = await BrewUpSerializer.SerializeAsync(@event, cancellationToken).ConfigureAwait(false);
        await _eventBus.PublishAsync(serializedMessage).ConfigureAwait(false);
    }
    
    private Task ProcessErrorAsync(ProcessErrorEventArgs arg)=> Task.CompletedTask;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _processor.StartProcessingAsync(cancellationToken);
    }
}