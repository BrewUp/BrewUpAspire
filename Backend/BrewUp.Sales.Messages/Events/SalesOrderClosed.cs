using BrewUp.Sales.SharedKernel.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Messages.Events;

public sealed class SalesOrderClosed(
    SalesOrderId aggregateId) : DomainEvent(aggregateId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
}
    