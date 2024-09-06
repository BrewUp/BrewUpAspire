namespace BrewUp.Shared.Events;

public record SalesOrderCreated(string OrderId, string OrderNumber,
	string CustomerId, string CustomerName,
	DateTime OrderDate,
	decimal TotalAmount, string Currency);