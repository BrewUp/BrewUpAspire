using Muflone.Core;

namespace BrewUp.Warehouses.Shared.DomainIds;

public class PurchaseOrderId : DomainId
{
	public PurchaseOrderId(Guid value) : base(value)
	{
	}
}