using BrewUp.Infrastructures.Azure;
using BrewUp.Infrastructures.RabbitMq;
using BrewUp.Sales.Azure.Infrastructures.Azure;
using BrewUp.Sales.Rmq.Infrastructures.RabbitMq;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class SalesHelper
{
    public static IServiceCollection AddSalesRmqInfrastructure(this IServiceCollection services,
        RabbitMqSettings rabbitMqSettings)
    {
        services.AddRabbitMqForSalesModule(rabbitMqSettings);
        
        return services;
    }
    
    public static IServiceCollection AddSalesAzureInfrastructure(this IServiceCollection services,
        AzureServiceBusSettings azureServiceBusSettings)
    {
        services.AddAzureServiceBusForSalesModule(azureServiceBusSettings);
        
        return services;
    }
}