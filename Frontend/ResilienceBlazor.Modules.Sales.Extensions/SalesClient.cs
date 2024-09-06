using BrewUp.Shared.Models;
using System.Net.Http.Json;

namespace ResilienceBlazor.Modules.Sales.Extensions;

public class SalesClient(HttpClient client)
{
	public async Task<IEnumerable<SalesOrder>> GetSalesOrdersAsync(CancellationToken cancellationToken)
		=> await client.GetFromJsonAsync<IEnumerable<SalesOrder>>("sales", cancellationToken)
		   ?? [];

	public async Task<string> CreateSalesOrderAsync(SalesOrder salesOrder, CancellationToken cancellationToken)
	{
		var response = await client.PostAsJsonAsync("sales", salesOrder, cancellationToken);
		return response.IsSuccessStatusCode ? "Success" : "Failed";
	}
}