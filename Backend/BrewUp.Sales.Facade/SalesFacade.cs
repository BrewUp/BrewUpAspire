using BrewUp.Sales.Messages.Commands;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Models;
using Muflone.Persistence;

namespace BrewUp.Sales.Facade;

public sealed class SalesFacade(
    IServiceBus serviceBus,
    ISalesOrderService salesOrderService)
    : ISalesFacade
{
    private readonly IServiceBus _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

    public async Task<string> CreateOrderAsync(SalesOrderJson body, CancellationToken cancellationToken)
    {
        if (body.OrderId.Equals(Guid.Empty))
            body = body with {OrderId = Guid.NewGuid()};
       
        CreateSalesOrder createSalesOrder = new(new SalesOrderId(body.OrderId),
            new SalesOrderNumber(body.OrderNumber),
            new CustomerId(body.CustomerId), new CustomerName(body.CustomerName), 
            new OrderDate(DateTime.Today), 
            body.Rows.Select(x => new SalesOrderRowDto(
                new BeerId(x.BeerId), new BeerName(x.BeerName),
                new Quantity(x.Quantity, "Lt"), 
                new Price(10, "EUR"))));

        await _serviceBus.SendAsync(createSalesOrder, cancellationToken);

        return body.OrderId.ToString();
    }
}