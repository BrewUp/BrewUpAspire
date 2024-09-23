using Microsoft.Extensions.Hosting;

namespace BrewUp.Sales.ReadModel;

public class SalesReadModelStarter(SalesOrderCreatedConsumer consumer) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await consumer.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}