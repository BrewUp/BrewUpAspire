using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Azure.Infrastructures.Azure.Events;

public sealed class SalesOrderCreatedConsumer(
    ISalesOrderService salesOrderService,
    AzureServiceBusConfiguration azureServiceBusConfiguration,
    ILoggerFactory loggerFactory)
    : DomainEventConsumerBase<SalesOrderCreated>(azureServiceBusConfiguration, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderCreated>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<SalesOrderCreated>>
    {
        new SalesOrderCreatedEventHandlerAsync(loggerFactory, salesOrderService)
    };
}