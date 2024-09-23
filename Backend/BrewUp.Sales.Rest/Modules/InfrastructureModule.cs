using BrewUp.Infrastructures;
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

        builder.Services.AddInfrastructureServices();
        builder.Services.AddSalesRmqInfrastructure(rabbitMqSettings);
        
        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}