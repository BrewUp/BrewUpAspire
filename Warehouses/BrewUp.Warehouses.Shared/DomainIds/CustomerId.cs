using Muflone.Core;

namespace BrewUp.Warehouses.Shared.DomainIds;

public sealed class CustomerId : DomainId
{
	public CustomerId(Guid value) : base(value)
	{
	}
}