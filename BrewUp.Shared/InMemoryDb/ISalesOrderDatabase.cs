using BrewUp.Shared.Models;

namespace BrewUp.Shared.InMemoryDb;

public interface ISalesOrderDatabase
{
	void AddSalesOrder(SalesOrder salesOrder);
	IEnumerable<SalesOrder> GetSalesOrders();
}