using Microsoft.Extensions.DependencyInjection;
using ResilienceBlazor.Shared.Configuration;
using System.Net.Http.Headers;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public static class SalesHelper
{
	public static IServiceCollection AddSalesModule(this IServiceCollection services, AppConfiguration configuration)
	{
		services.AddScoped<ISalesService, SalesService>();
		services.AddHttpClient<SalesClient>(client =>
		{
			client.BaseAddress = new Uri(configuration.BrewUpSalesUri);
			//client.BaseAddress = new Uri("https://brewup-sales");
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		});

		return services;
	}
}