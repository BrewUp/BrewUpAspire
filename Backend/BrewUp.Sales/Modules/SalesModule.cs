using Azure.Messaging.ServiceBus;
using BrewUp.Sales.Services;
using BrewUp.Shared;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Models;
using System.Text.Json;

namespace BrewUp.Sales.Modules
{
	public sealed class SalesModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddShared();
			builder.Services.AddHostedService<SalesReadModelWorker>();
			builder.Services.AddScoped<SalesService>();

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var salesGroup = endpoints.MapGroup("sales")
				.WithTags("Sales");

			salesGroup.MapGet("", GetSaleOrdersAsync)
				.Produces(StatusCodes.Status202Accepted)
				.ProducesValidationProblem()
				.WithName("GetSaleOrders")
				.WithTags("Sales");

			salesGroup.MapPost("", HandleCreateSalesOrderAsync);

			return endpoints;
		}

		private static async Task<IResult> GetSaleOrdersAsync(SalesService salesService)
		{
			var salesOrder = await salesService.GetSaleOrdersAsync();
			return Results.Ok(salesOrder);
		}

		private static async Task<IResult> HandleCreateSalesOrderAsync(
			SalesOrder body,
			ServiceBusClient serviceBusClient)
		{
			var sender = serviceBusClient.CreateSender("createsalesorder");

			body = body with
			{
				OrderId = Guid.NewGuid(),
				CustomerName = "Il Grottino del Muflone",
				OrderNumber = GetSalesOrderNumber(),
				TotalAmount = GetSalesOrderTotalAmount()
			};

			// Create a command
			CreateSalesOrder command = new(body.OrderId.ToString(), body.OrderNumber,
				body.CustomerId.ToString(), body.CustomerName,
				DateTime.UtcNow,
				body.TotalAmount, "EUR");

			await sender.SendMessageAsync(new ServiceBusMessage(JsonSerializer.Serialize(command)), CancellationToken.None);

			Console.WriteLine($"SalesOrder number {command.OrderNumber} has been sent");

			return Results.Accepted();
		}

		private static string GetSalesOrderNumber()
		{
			var reference = DateTime.UtcNow;
			return
				$"{reference.Year:0000}{reference.Month:00}{reference.Day:00}-{reference.Hour:00}{reference.Minute:00}{reference.Second:00}";
		}

		private static decimal GetSalesOrderTotalAmount()
		{
			var random = new Random(200);
			var totalAmount = random.Next(1000, 10000);
			return totalAmount + DateTime.UtcNow.Second;
		}
	}
}