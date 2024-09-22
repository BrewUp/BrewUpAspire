using Microsoft.Extensions.Hosting;

namespace BrewUp.Sales.Domain;

public class SalesDomainStarter(BrewUpConsumer brewUpConsumer) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await brewUpConsumer.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}