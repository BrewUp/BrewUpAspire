using BrewUp.Shared.Models;

namespace BrewUp.Shared.Events;

public class SalesOrderCreated(
    Guid salesOrderId,
    string salesOrderNumber,
    Guid customerId,
    string customerName,
    decimal totalAmount,
    string currency,
    IEnumerable<SalesRow> rows)
{
    public readonly Guid SalesOrderId = salesOrderId;
    public readonly string SalesOrderNumber = salesOrderNumber;
    public readonly Guid CustomerId = customerId;
    public readonly string CustomerName = customerName;
    public readonly decimal TotalAmount = totalAmount;
    public readonly string Currency = currency;
    public readonly IEnumerable<SalesRow> Rows = rows;
}