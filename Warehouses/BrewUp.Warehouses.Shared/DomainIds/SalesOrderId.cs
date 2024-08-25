using Muflone.Core;

namespace BrewUp.Warehouses.Shared.DomainIds;

public sealed class SalesOrderId : DomainId
{
	public SalesOrderId(Guid value) : base(value)
	{
	}
}