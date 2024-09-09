using BrewUp.Shared.Models;

namespace BrewUp.Shared.InMemoryDb;

public interface IRepository
{
    void AddSalesOrder(SalesOrder salesOrder);
    IEnumerable<SalesOrder> GetSalesOrders();
}