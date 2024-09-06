using BrewUp.Shared.Models;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public sealed class SalesService(SalesClient salesClient) : ISalesService
{
	public async Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(CancellationToken cancellationToken) =>
		await salesClient.GetSalesOrdersAsync(cancellationToken);

	public async Task<string> CreateSalesOrderAsync(SalesOrder salesOrder, CancellationToken cancellationToken) =>
			await salesClient.CreateSalesOrderAsync(salesOrder, cancellationToken);
}