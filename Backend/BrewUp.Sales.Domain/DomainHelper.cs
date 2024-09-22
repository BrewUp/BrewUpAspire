using BrewUp.Sales.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Domain;

public static class DomainHelper
{
    public static IServiceCollection AddSalesDomain(this IServiceCollection services,
        AzureServiceBusConfiguration azureServiceBusConfiguration)
    {
        services.AddSingleton<BrewUpConsumer>();
        services.AddSingleton(new AzureServiceBus(azureServiceBusConfiguration));
        services.AddSingleton(new AzureEventBus(azureServiceBusConfiguration));
        services.AddSingleton(azureServiceBusConfiguration);

        services.AddHostedService<SalesDomainStarter>();
        
        return services;
    }
}