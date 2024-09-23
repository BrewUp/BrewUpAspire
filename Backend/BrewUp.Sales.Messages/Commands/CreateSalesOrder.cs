using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Sales.Messages.Commands;

public sealed class CreateSalesOrder(
    SalesOrderId aggregateId,
    SalesOrderNumber salesOrderNumber,
    CustomerId customerId,
    CustomerName customerName,
    OrderDate orderDate,
    IEnumerable<SalesOrderRowDto> lines)
    : Command(aggregateId)
{
    public readonly SalesOrderId SalesOrderId = aggregateId;
    public readonly SalesOrderNumber SalesOrderNumber = salesOrderNumber;
    
    public readonly CustomerId CustomerId = customerId;
    public readonly CustomerName CustomerName = customerName;
    
    public readonly OrderDate OrderDate = orderDate;

    public readonly IEnumerable<SalesOrderRowDto> Lines = lines;
}