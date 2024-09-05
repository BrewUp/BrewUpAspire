using BrewUp.Warehouses.Models;

namespace BrewUp.Warehouses.Services;

public class WarehousesService
{
	public async Task<Availability> GetAvailabilityAsync()
	{
		return await Task.FromResult(new Availability(Guid.NewGuid().ToString(), "Muflone IPA", 42, "bottles"));
	}
}