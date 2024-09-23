using System.Collections.ObjectModel;
using Muflone;
using Muflone.Core;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace BrewUp.Infrastructures.EventStore;

public sealed class InMemoryEventRepository : IRepository
{
	private IEnumerable<DomainEvent> _givenEvents = [];
	
	public static ObservableCollection<DomainEvent> Events { get; private set; } = [];

	private static TAggregate? ConstructAggregate<TAggregate>()
	{
		return (TAggregate?)Activator.CreateInstance(typeof(TAggregate), true);
	}

	public void Dispose()
	{
		// no op
	}

	public void ApplyGivenEvents(IList<DomainEvent> events)
	{
		_givenEvents = events;
	}

	public async Task<TAggregate?> GetByIdAsync<TAggregate>(IDomainId id, CancellationToken cancellationToken) where TAggregate : class, IAggregate
	{
		return await GetByIdAsync<TAggregate>(id, 0, cancellationToken);
	}

	public Task<TAggregate?> GetByIdAsync<TAggregate>(IDomainId id, long version, CancellationToken cancellationToken) where TAggregate : class, IAggregate
	{
		var aggregate = ConstructAggregate<TAggregate>();
		if (aggregate is not null)
			_givenEvents.ForEach(aggregate.ApplyEvent);
		return Task.FromResult(aggregate);
	}

	public async Task SaveAsync(IAggregate aggregate, Guid commitId, CancellationToken cancellationToken = default)
	{
		await SaveAsync(aggregate, commitId, null);
	}

	public Task SaveAsync(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders, CancellationToken cancellationToken = default)
	{
		var events = aggregate.GetUncommittedEvents().Cast<DomainEvent>();
		foreach (var @event in events)
		{
			Events.Add(@event);
		}
		return Task.CompletedTask;
	}
}

public static class Helpers
{
	//Stolen from Castle.Core.Internal
	public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
	{
		if (items == null)
			return;
		foreach (T obj in items)
			action(obj);
	}
}