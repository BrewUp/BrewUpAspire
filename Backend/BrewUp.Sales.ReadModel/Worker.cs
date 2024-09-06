using Azure.Messaging.ServiceBus;
using BrewUp.Shared.Events;
using System.Text.Json;

namespace BrewUp.Sales.ReadModel;

public class Worker(ILogger<Worker> logger,
	ServiceBusClient serviceBusClient) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			var processor = serviceBusClient.CreateProcessor(
				"salesordercreated",
				"sales",
				new ServiceBusProcessorOptions());

			// Add handler to process messages
			processor.ProcessMessageAsync += SalesOrderCreatedEventHandler;

			// Add handler to process any errors
			processor.ProcessErrorAsync += ErrorHandler;

			// Start processing
			await processor.StartProcessingAsync(stoppingToken);

			// Wait for a minute and then press any key to end the processing
			Thread.Sleep(60000);

			// Stop processing
			await processor.StopProcessingAsync(stoppingToken);

			logger.LogInformation("Stopped receiving messages");
		}
	}

	private async Task SalesOrderCreatedEventHandler(ProcessMessageEventArgs args)
	{
		var body = args.Message.Body.ToString();
		var salesOrder = JsonSerializer.Deserialize<SalesOrderCreated>(body);

		logger.LogInformation($"SalesOrder Received: {salesOrder.OrderNumber} from subscription.", body);

		// Complete the message. Message is deleted from the subscription.
		await args.CompleteMessageAsync(args.Message);
	}

	// Handle any errors when receiving messages
	private Task ErrorHandler(ProcessErrorEventArgs args)
	{
		logger.LogError(args.Exception, "{Error}", args.Exception.Message);

		return Task.CompletedTask;
	}
}