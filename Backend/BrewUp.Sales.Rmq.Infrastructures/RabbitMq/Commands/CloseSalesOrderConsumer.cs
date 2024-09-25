using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;

namespace BrewUp.Sales.Rmq.Infrastructures.RabbitMq.Commands;

public sealed class CloseSalesOrderConsumer(
    IRepository repository,
    IRabbitMQConnectionFactory connectionFactory,
    ILoggerFactory loggerFactory)
    : CommandConsumerBase<CloseSalesOrder>(repository, connectionFactory, loggerFactory)
{
    protected override ICommandHandlerAsync<CloseSalesOrder> HandlerAsync { get; } = new CloseSalesOrderCommandHandlerAsync(repository, loggerFactory);
}