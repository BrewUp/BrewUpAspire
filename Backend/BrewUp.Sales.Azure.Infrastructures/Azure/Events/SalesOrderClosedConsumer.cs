using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Azure.Infrastructures.Azure.Events;

public sealed class SalesOrderClosedConsumer(
    ISalesOrderService salesOrderService,
    AzureServiceBusConfiguration azureServiceBusConfiguration,
    ILoggerFactory loggerFactory)
    : DomainEventConsumerBase<SalesOrderClosed>(azureServiceBusConfiguration, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderClosed>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<SalesOrderClosed>>
    {
        new SalesOrderClosedEventHandlerAsync(loggerFactory, salesOrderService)
    };
}