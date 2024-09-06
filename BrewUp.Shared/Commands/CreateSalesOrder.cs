namespace BrewUp.Shared.Commands;

public record CreateSalesOrder(string OrderId, string OrderNumber,
	string CustomerId, string CustomerName,
	DateTime OrderDate,
	decimal TotalAmount, string Currency);