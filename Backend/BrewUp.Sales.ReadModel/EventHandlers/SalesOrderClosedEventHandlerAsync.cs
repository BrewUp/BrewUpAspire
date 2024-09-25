using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;

namespace BrewUp.Sales.ReadModel.EventHandlers;

public sealed class SalesOrderClosedEventHandlerAsync(
    ILoggerFactory loggerFactory,
    ISalesOrderService salesOrderService)
    : DomainEventHandlerBase<SalesOrderClosed>(loggerFactory)
{
    public override async Task HandleAsync(SalesOrderClosed @event, CancellationToken cancellationToken = new ())
    {
        try
        {
            await salesOrderService.CloseSalesOrderAsync(@event.SalesOrderId, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error handling sales order closed event");
            throw;
        }
    }
}