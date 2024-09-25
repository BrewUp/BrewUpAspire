using BrewUp.Sales.SharedKernel.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.Messages.Commands;

public sealed class CloseSalesOrder(
    SalesOrderId aggregateId)
    : Command(aggregateId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
}