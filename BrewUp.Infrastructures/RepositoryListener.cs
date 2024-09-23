using System.Collections.Specialized;
using BrewUp.Infrastructures.EventStore;
using Microsoft.Extensions.Hosting;
using Muflone;
using Muflone.Messages.Events;

namespace BrewUp.Infrastructures;

public class RepositoryListener(IEventBus eventBus) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        InMemoryEventRepository.Events.CollectionChanged += EventsOnCollectionChanged;
        
        return Task.CompletedTask;
    }

    private void EventsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action != NotifyCollectionChangedAction.Add)
            return;

        foreach (var item in e.NewItems!)
        {
            eventBus.PublishAsync((IDomainEvent)item, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}