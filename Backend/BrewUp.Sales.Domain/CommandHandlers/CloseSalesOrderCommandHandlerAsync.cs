using BrewUp.Sales.Domain.Entities;
using BrewUp.Sales.Messages.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUp.Sales.Domain.CommandHandlers;

public class CloseSalesOrderCommandHandlerAsync(IRepository repository, ILoggerFactory loggerFactory)
    : CommandHandlerBaseAsync<CloseSalesOrder>(repository, loggerFactory)
{
    public override async Task ProcessCommand(CloseSalesOrder command, CancellationToken cancellationToken = default)
    {
        var aggregate = await Repository.GetByIdAsync<SalesOrder>(command.AggregateId, cancellationToken);
        aggregate!.CloseOrder();
            
        await Repository.SaveAsync(aggregate, Guid.NewGuid(), cancellationToken);
    }
}