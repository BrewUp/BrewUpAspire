using BrewUp.Warehouses.Shared.CustomTypes;
using BrewUp.Warehouses.Shared.DomainIds;
using Muflone.Messages.Events;

namespace BrewUp.Warehouses.Shared.Messages.Sagas;

public sealed class BeerAvailabilityCommunicated : IntegrationEvent
{
	public readonly BeerId BeerId;
	public readonly Availability Availability;

	public BeerAvailabilityCommunicated(BeerId aggregateId, Guid correlationId, Availability availability) : base(aggregateId, correlationId)
	{
		BeerId = aggregateId;
		Availability = availability;
	}
}