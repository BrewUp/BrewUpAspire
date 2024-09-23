using System.Text.Json.Serialization;

namespace BrewUp.Shared.Models;

public record SalesOrderJson(
	Guid OrderId,
	string OrderNumber,
	Guid CustomerId,
	string CustomerName,
	decimal TotalAmount,
	string Currency,
	IEnumerable<SalesRow> Rows);

[JsonSerializable(typeof(List<SalesOrderJson>))]
public sealed partial class SalesOrderSerializerContext : JsonSerializerContext
{
}