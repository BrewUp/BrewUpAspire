using Muflone.Core;

namespace BrewUp.Warehouses.Shared.DomainIds;

public sealed class ProductionOrderId : DomainId
{
	public ProductionOrderId(Guid value) : base(value)
	{
	}
}