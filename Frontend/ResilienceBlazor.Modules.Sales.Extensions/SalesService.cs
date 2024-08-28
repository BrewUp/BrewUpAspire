using BrewUp.Shared.Models;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public sealed class SalesService(SalesClient salesClient) : ISalesService
{
	public async Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(CancellationToken cancellationToken) =>
		await salesClient.GetSalesOrdersAsync(cancellationToken);
}