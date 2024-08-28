using Microsoft.OpenApi.Models;

namespace BrewUp.Sales.Modules
{
	public sealed class SwaggerModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
			{
				Description = "Sales API",
				Title = "Sales Api",
				Version = "v1",
				Contact = new OpenApiContact
				{
					Name = "Sales Api"
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