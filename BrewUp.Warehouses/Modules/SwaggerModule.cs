using Microsoft.OpenApi.Models;

namespace BrewUp.Warehouses.Modules
{
	public sealed class SwaggerModule : IModule
	{
		public bool IsEnabled { get; }
		public int Order { get; }

		public SwaggerModule()
		{
			IsEnabled = true;
			Order = 0;
		}

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo
			{
				Description = "Warehouses API",
				Title = "BrewUp Api",
				Version = "v1",
				Contact = new OpenApiContact
				{
					Name = "Warehouses.Api"
				}
			}));

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			return endpoints;
		}
	}
}