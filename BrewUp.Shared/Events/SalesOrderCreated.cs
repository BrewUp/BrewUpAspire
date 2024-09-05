namespace BrewUp.Shared.Events;

public record SalesOrderCreated(string OrderId, string OrderNumber, DateTime OrderDate, decimal TotalAmount);