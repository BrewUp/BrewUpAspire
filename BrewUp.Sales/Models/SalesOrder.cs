namespace BrewUp.Sales.Models
{
	public record SalesOrder(
		Guid OrderId,
		string OrderNumber,
		Guid CustomerId,
		string CustomerName,
		decimal TotalAmount,
		string Currency,
		IEnumerable<SalesRow> Rows);
}