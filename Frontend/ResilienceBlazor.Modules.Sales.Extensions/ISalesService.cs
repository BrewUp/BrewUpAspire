using BrewUp.Shared.Models;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public interface ISalesService
{
	Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(CancellationToken cancellationToken);
	Task<string> CreateSalesOrderAsync(SalesOrder salesOrder, CancellationToken cancellationToken);
}