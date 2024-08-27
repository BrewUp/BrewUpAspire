using BrewUp.Warehouses.Services;

namespace BrewUp.Warehouses.Modules
{
	public sealed class WarehousesModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddScoped<WarehousesService>();

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints.MapGet("/warehouses", GetAvailabilityAsync)
				.Produces(StatusCodes.Status200OK)
				.WithName("GetAvailability")
				.WithTags("Warehouses");

			return endpoints;
		}

		private static async Task<IResult> GetAvailabilityAsync(WarehousesService warehousesService)
		{
			var availability = await warehousesService.GetAvailabilityAsync();

			return Results.Ok(availability);
		}
	}
}