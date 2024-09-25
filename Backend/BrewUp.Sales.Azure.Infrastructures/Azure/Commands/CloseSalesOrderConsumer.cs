using BrewUp.Sales.Domain.CommandHandlers;
using BrewUp.Sales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUp.Sales.Azure.Infrastructures.Azure.Commands;

public sealed class CloseSalesOrderConsumer(
    IRepository repository,
    AzureServiceBusConfiguration azureServiceBusConfiguration,
    ILoggerFactory loggerFactory)
    : CommandConsumerBase<CloseSalesOrder>(azureServiceBusConfiguration, loggerFactory)
{
    protected override ICommandHandlerAsync<CloseSalesOrder> HandlerAsync { get; } = new CloseSalesOrderCommandHandlerAsync(repository, loggerFactory);
}