using BrewUp.Warehouses.Shared.CustomTypes;
using BrewUp.Warehouses.Shared.DomainIds;
using Muflone.Messages.Commands;

namespace BrewUp.Warehouses.SharedKernel.Commands;

public sealed class UpdateAvailabilityDueToSalesOrder(BeerId aggregateId, Guid commitId, Quantity quantity)
	: Command(aggregateId, commitId)
{
	public readonly BeerId BeerId = aggregateId;
	public readonly Quantity Quantity = quantity;
}