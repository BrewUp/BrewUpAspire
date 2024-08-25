using Muflone.Core;

namespace BrewUp.Warehouses.Shared.DomainIds;

public sealed class OrderId : DomainId
{
	public OrderId(Guid value) : base(value)
	{
	}
}