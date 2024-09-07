using BrewUp.Shared.Models;

namespace BrewUp.Shared.InMemoryDb;

public class Repository(ISalesOrderDatabase salesOrderDatabase)
{
	public void AddSalesOrder(SalesOrder salesOrder)
	{
		salesOrderDatabase.AddSalesOrder(salesOrder);
	}

	public IEnumerable<SalesOrder> GetSalesOrders() => salesOrderDatabase.GetSalesOrders();
}