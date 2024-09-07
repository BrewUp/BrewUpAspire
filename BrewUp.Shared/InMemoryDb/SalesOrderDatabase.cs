using BrewUp.Shared.Models;

namespace BrewUp.Shared.InMemoryDb;

public class SalesOrderDatabase : ISalesOrderDatabase
{
	private IEnumerable<SalesOrder> SalesOrders { get; set; } = [];

	public SalesOrderDatabase()
	{ }

	public void AddSalesOrder(SalesOrder salesOrder)
	{
		SalesOrders = SalesOrders.Append(salesOrder);
	}

	public IEnumerable<SalesOrder> GetSalesOrders() => SalesOrders;
}