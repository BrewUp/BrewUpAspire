using BrewUp.Sales.Domain;
using BrewUp.Sales.ReadModel;
using BrewUp.Sales.Services;

namespace BrewUp.Sales.Modules
{
	public sealed class SalesModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			AzureServiceBusConfiguration config = new (
				builder.Configuration["BrewUp:AzureServiceBus:ConnectionString"]!,
				"createsalesorder", "sales");
			builder.Services.AddScoped<SalesService>();
			builder.Services.AddSalesDomain(config);
			builder.Services.AddSalesReadModel(config);

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var salesGroup = endpoints.MapGroup("sales")
				.WithTags("Sales");
			
			salesGroup.MapPost("", CreateSaleOrdersAsync)
				.Produces(StatusCodes.Status202Accepted)
				.ProducesValidationProblem()
				.WithName("CreateSaleOrder");
			
			salesGroup.MapGet("", GetSaleOrdersAsync)
				.Produces(StatusCodes.Status202Accepted)
				.ProducesValidationProblem()
				.WithName("GetSaleOrders");

			return endpoints;
		}

		private static async Task<IResult> CreateSaleOrdersAsync(SalesService salesService)
		{
			var orderId = await salesService.CreateSalesOrderAsync();
			
			return Results.Created("", orderId);
		}

		private static async Task<IResult> GetSaleOrdersAsync(SalesService salesService)
		{
			var salesOrder = await salesService.GetSaleOrdersAsync();
			return Results.Ok(salesOrder);
		}
	}
}