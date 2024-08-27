using BrewUp.Sales.Services;

namespace BrewUp.Sales.Modules
{
	public sealed class SalesModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<SalesService>();

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapGet("/sales", GetSaleOrdersAsync)
				.Produces(StatusCodes.Status202Accepted)
				.ProducesValidationProblem()
				.WithName("GetSaleOrders")
				.WithTags("Sales");

			return endpoints;
		}

		private static async Task<IResult> GetSaleOrdersAsync(SalesService salesService)
		{
			var salesOrder = await salesService.GetSaleOrdersAsync();
			return Results.Ok(salesOrder);
		}
	}
}