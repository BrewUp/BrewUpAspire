
using Azure.Messaging.ServiceBus;
using BrewUp.Shared.Events;
using BrewUp.Shared.InMemoryDb;
using BrewUp.Shared.Models;
using System.Text.Json;

namespace BrewUp.Sales;

public class SalesReadModelWorker(ILogger<SalesReadModelWorker> logger,
    IRepository repository,
    ServiceBusClient serviceBusClient) : IHostedService
{
    private ServiceBusProcessor _processor;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _processor = serviceBusClient.CreateProcessor(
                "salesordercreated",
                "sales",
                new ServiceBusProcessorOptions());
        // Add handler to process messages
        _processor.ProcessMessageAsync += SalesOrderCreatedEventHandler;

        // Add handler to process any errors
        _processor.ProcessErrorAsync += ErrorHandler;

        // Start processing
        await _processor.StartProcessingAsync(cancellationToken);
    }

    private async Task SalesOrderCreatedEventHandler(ProcessMessageEventArgs args)
    {
        var body = args.Message.Body.ToString();
        var salesOrderCreated = JsonSerializer.Deserialize<SalesOrderCreated>(body);

        if (salesOrderCreated != null)
        {
            SalesOrder salesOrder = new(new Guid(salesOrderCreated.OrderId), salesOrderCreated.OrderNumber,
                new Guid(salesOrderCreated.CustomerId), salesOrderCreated.CustomerName,
                salesOrderCreated.TotalAmount, salesOrderCreated.Currency,
                []);
            repository.AddSalesOrder(salesOrder);
        }

        logger.LogInformation($"SalesOrder Received: {salesOrderCreated!.OrderNumber} from subscription.", body);

        // Complete the message. Message is deleted from the subscription.
        await args.CompleteMessageAsync(args.Message);
    }

    // Handle any errors when receiving messages
    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        logger.LogError(args.Exception, "{Error}", args.Exception.Message);

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop processing
        await _processor.StopProcessingAsync(cancellationToken);
    }
}
