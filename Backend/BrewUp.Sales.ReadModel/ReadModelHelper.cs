using BrewUp.Sales.Domain;
using BrewUp.Sales.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.ReadModel;

public static class ReadModelHelper
{
    public static IServiceCollection AddSalesReadModel(this IServiceCollection services,
        AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        services.AddSingleton<SalesOrderCreatedConsumer>();
        services.AddSingleton(new AzureServiceBus(azureServiceBusConfiguration));
        services.AddSingleton(new AzureEventBus(azureServiceBusConfiguration));
        services.AddSingleton(azureServiceBusConfiguration);

        services.AddHostedService<SalesReadModelStarter>();
        
        return services;
    }
}