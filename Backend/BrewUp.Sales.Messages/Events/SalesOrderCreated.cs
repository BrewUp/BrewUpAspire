using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Sales.Messages.Events;

public sealed class SalesOrderCreated(
    SalesOrderId aggregateId,
    SalesOrderNumber salesOrderNumber,
    CustomerId customerId,
    CustomerName customerName,
    OrderDate orderDate,
    IEnumerable<SalesOrderRowDto> rows)
    : DomainEvent(aggregateId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;
    public readonly CustomerId CustomerId = customerId;
    public readonly CustomerName CustomerName = customerName;
    public readonly OrderDate OrderDate = orderDate;

    public readonly IEnumerable<SalesOrderRowDto> Rows = rows;
}