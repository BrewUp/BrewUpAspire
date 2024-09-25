using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.Services;

public sealed class SalesOrderService(ILoggerFactory loggerFactory) : ServiceBase(loggerFactory), ISalesOrderService
{
    public Task CreateSalesOrderAsync(SalesOrderId salesOrderId, SalesOrderNumber salesOrderNumber, CustomerId customerId,
        CustomerName customerName, OrderDate orderDate, IEnumerable<SalesOrderRowDto> rows, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task CloseSalesOrderAsync(SalesOrderId salesOrderId, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}