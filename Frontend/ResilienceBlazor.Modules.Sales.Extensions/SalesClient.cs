using BrewUp.Shared.Models;
using System.Net.Http.Json;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public class SalesClient(HttpClient client)
{
	public async Task<IEnumerable<SalesOrderJson>> GetSalesOrdersAsync(CancellationToken cancellationToken)
		=> await client.GetFromJsonAsync<IEnumerable<SalesOrderJson>>("sales", cancellationToken)
		   ?? Enumerable.Empty<SalesOrderJson>();
}