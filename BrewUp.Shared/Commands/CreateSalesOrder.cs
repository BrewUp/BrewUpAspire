namespace BrewUp.Shared.Commands;

public record CreateSalesOrder(string OrderId, string OrderNumber, DateTime OrderDate, decimal TotalAmount);