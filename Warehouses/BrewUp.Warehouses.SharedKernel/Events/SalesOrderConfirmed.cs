using BrewUp.Warehouses.Shared.Contracts;
using BrewUp.Warehouses.Shared.CustomTypes;
using BrewUp.Warehouses.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.SharedKernel.Events;

public sealed class SalesOrderConfirmed(SalesOrderId aggregateId, Guid commitId, SalesOrderNumber salesOrderNumber,
	IEnumerable<SalesOrderRowJson> rows) : IntegrationEvent(aggregateId, commitId)
{
	public readonly SalesOrderId SalesOrderId = aggregateId;
	public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;

	public readonly IEnumerable<SalesOrderRowJson> Rows = rows;
}