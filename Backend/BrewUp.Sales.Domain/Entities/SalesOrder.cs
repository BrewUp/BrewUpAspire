
using BrewUp.Sales.Domain.Helpers;
using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Muflone.Core;

namespace BrewUp.Sales.Domain.Entities;

public sealed class SalesOrder : AggregateRoot
{
    private SalesOrderNumber _salesOrderNumber = new SalesOrderNumber(Guid.NewGuid().ToString());
    
    private CustomerId _customerId;
    private CustomerName _customerName;
    
    private OrderDate _orderDate;

    private IEnumerable<SalesOrderLine> _lines;

    private Status _status;

    protected SalesOrder()
    {
    }

    #region Create
    internal static SalesOrder CreateSalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber,
        CustomerId customerId, CustomerName customerName, OrderDate orderDate, IEnumerable<SalesOrderRowDto> lines) =>
        new(salesOrderId, salesOrderNumber, customerId, customerName, orderDate, lines);

    private SalesOrder(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, CustomerId customerId, CustomerName customerName,
        OrderDate orderDate, IEnumerable<SalesOrderRowDto> lines)
    {
        // Here you must check the aggregate invariants
        RaiseEvent(new SalesOrderCreated(salesOrderId, salesOrderNumber, customerId, customerName, orderDate, lines ));
    }

    private void Apply(SalesOrderCreated @event)
    {
        Id = @event.SalesOrderId;
        
        _salesOrderNumber = @event.SalesOrderNumber;
        
        _customerId = @event.CustomerId;
        _customerName = @event.CustomerName;
        
        _orderDate = @event.OrderDate;
        
        _lines = @event.Rows.ToDomainEntities();
        
        _status = Status.Created;
    }
    #endregion
}