using BrewUp.Shared.Models;

namespace BrewUp.Sales.Services;

public class SalesService
{
	public async Task<IEnumerable<SalesOrder>> GetSaleOrdersAsync()
	{
		return await Task.FromResult(new List<SalesOrder>
		{
			new (
				Guid.NewGuid(),
				"SO-0001",
				Guid.NewGuid(),
				"John Doe",
				42.0m,
				"USD",
				new List<SalesRow>
				{
					new(Guid.NewGuid(), "Muflone IPA", 6),
					new(Guid.NewGuid(), "Muflone APA", 6),
					new(Guid.NewGuid(), "Muflone Stout", 6),
					new(Guid.NewGuid(), "Muflone Pilsner", 6),
					new(Guid.NewGuid(), "Muflone Lager", 6),
					new(Guid.NewGuid(), "Muflone Weizen", 6)
				}
			)
		});
	}
}