using BrewUp.Sales.Messages.Events;
using BrewUp.Sales.ReadModel.EventHandlers;
using BrewUp.Sales.ReadModel.Services;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Rmq.Infrastructures.RabbitMq.Events;

public sealed class SalesOrderClosedConsumer(
    ISalesOrderService salesOrderService,
    IRabbitMQConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory)
    : DomainEventsConsumerBase<SalesOrderClosed>(connectionFactory, loggerFactory)
{
    protected override IEnumerable<IDomainEventHandlerAsync<SalesOrderClosed>> HandlersAsync { get; } = new List<DomainEventHandlerAsync<SalesOrderClosed>>
    {
        new SalesOrderClosedEventHandlerAsync(loggerFactory, salesOrderService)
    };
}