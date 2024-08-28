using System.Text.Json.Serialization;

namespace BrewUp.Shared.Models;

public record SalesOrder(
	Guid OrderId,
	string OrderNumber,
	Guid CustomerId,
	string CustomerName,
	decimal TotalAmount,
	string Currency,
	IEnumerable<SalesRow> Rows);

[JsonSerializable(typeof(List<SalesOrder>))]
public sealed partial class SalesOrderSerializerContext : JsonSerializerContext
{
}