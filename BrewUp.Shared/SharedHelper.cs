using BrewUp.Shared.InMemoryDb;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<ISalesOrderDatabase, SalesOrderDatabase>();
        services.AddSingleton<IRepository, Repository>();

        return services;
    }
}
