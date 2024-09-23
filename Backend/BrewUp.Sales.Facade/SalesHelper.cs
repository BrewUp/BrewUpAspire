using BrewUp.Infrastructures.Azure;
using BrewUp.Infrastructures.RabbitMq;
using BrewUp.Sales.Azure.Infrastructures.Azure;
using BrewUp.Sales.Facade.Validators;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.Rmq.Infrastructures.RabbitMq;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Sales.Facade;

public static class SalesHelper
{
    public static IServiceCollection AddSales(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<SalesOrderValidator>();
        services.AddSingleton<ValidationHandler>();
        
        services.AddScoped<ISalesFacade, SalesFacade>();
        services.AddScoped<ISalesOrderService, SalesOrderService>();

        return services;
    }
    
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