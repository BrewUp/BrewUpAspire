using BrewUp.Shared.InMemoryDb;
using Microsoft.Extensions.DependencyInjection;

namespace BrewUp.Shared;

public static class SharedHelper
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        var salesOrderDatabase = services.BuildServiceProvider().GetService<ISalesOrderDatabase>();
        if (salesOrderDatabase is not null)
            return services;

        services.AddSingleton<ISalesOrderDatabase, SalesOrderDatabase>();
        services.AddSingleton<Repository>();

        return services;
    }
}
