using Azure.Messaging.ServiceBus;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Events;
using System.Text.Json;

namespace BrewUp.Sales.Domain;

public sealed class Worker(ILogger<Worker> logger,
	ServiceBusClient serviceBusClient) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var processor = serviceBusClient.CreateProcessor(
				"createsalesorder",
				"",
				new ServiceBusProcessorOptions());

			// Add handler to process messages
			processor.ProcessMessageAsync += CreateSalesOrderCommandHandler;

			//// Add handler to process any errors
			processor.ProcessErrorAsync += ErrorHandler;

			// Start processing
			await processor.StartProcessingAsync(stoppingToken);

			// Wait for a minute and then press any key to end the processing
			Thread.Sleep(60000);

			await processor.StopProcessingAsync(stoppingToken);

			logger.LogInformation("Stopped receiving messages");
		}
	}

	private async Task CreateSalesOrderCommandHandler(ProcessMessageEventArgs args)
	{
		var body = args.Message.Body.ToString();
		var salesOrder = JsonSerializer.Deserialize<CreateSalesOrder>(body);

		logger.LogInformation($"SalesOrder Received: {salesOrder.OrderNumber} from subscription.", body);

		await RaiseSalesOrderCreatedAsync(salesOrder);

		// Complete the message. messages is deleted from the subscription.
		await args.CompleteMessageAsync(args.Message);
	}

	// Handle any errors when receiving messages
	private Task ErrorHandler(ProcessErrorEventArgs args)
	{
		logger.LogError(args.Exception, "{Error}", args.Exception.Message);

		return Task.CompletedTask;
	}

	private async Task RaiseSalesOrderCreatedAsync(CreateSalesOrder salesOrder)
	{
		var sender = serviceBusClient.CreateSender("salesordercreated");

		SalesOrderCreated domainEvent = new(salesOrder.OrderId, salesOrder.OrderNumber,
			salesOrder.CustomerId, salesOrder.CustomerName,
			salesOrder.OrderDate,
			salesOrder.TotalAmount, salesOrder.Currency);

		await sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(domainEvent)), CancellationToken.None);
	}
}