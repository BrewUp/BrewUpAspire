using BrewUp.Shared.Models;

namespace BrewUp.Sales.Facade;

public interface ISalesFacade
{
    Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken);
}