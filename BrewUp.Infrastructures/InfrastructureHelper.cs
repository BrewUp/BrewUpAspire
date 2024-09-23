using BrewUp.Infrastructures.EventStore;
using Microsoft.Extensions.DependencyInjection;
using Muflone.Persistence;

namespace BrewUp.Infrastructures;

public static class InfrastructureHelper
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IRepository, InMemoryEventRepository>();
        services.AddHostedService<RepositoryListener>();
        
        return services;
    }
}