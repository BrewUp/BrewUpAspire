using BrewUp.Infrastructures;
using BrewUp.Infrastructures.Azure;
using BrewUp.Infrastructures.RabbitMq;
using BrewUp.Sales.Facade;

namespace BrewUp.Sales.Rest.Modules;

public sealed class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 90;
    
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var rabbitMqSettings = builder.Configuration.GetSection("BrewUp:RabbitMQ")
            .Get<RabbitMqSettings>()!;
        
        var azureServiceBusSettings = builder.Configuration.GetSection("BrewUp:AzureServiceBus")
            .Get<AzureServiceBusSettings>()!;

        builder.Services.AddInfrastructureServices();
        if (builder.Configuration["BrewUp:Broker"]!.Equals("RMQ"))
            builder.Services.AddSalesRmqInfrastructure(rabbitMqSettings);
        
        if (builder.Configuration["BrewUp:Broker"]!.Equals("AzureServiceBus"))
            builder.Services.AddSalesAzureInfrastructure(azureServiceBusSettings);
        
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}