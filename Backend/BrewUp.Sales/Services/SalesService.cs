using BrewUp.Shared.InMemoryDb;
using BrewUp.Shared.Models;

namespace BrewUp.Sales.Services;

public class SalesService(IRepository repository)
{
	public async Task<IEnumerable<SalesOrder>> GetSaleOrdersAsync() =>
		await Task.FromResult(repository.GetSalesOrders());
}