using BrewUp.Warehouses.Shared.Contracts;
using BrewUp.Warehouses.Shared.CustomTypes;

namespace BrewUp.Warehouses.Shared.Helpers;

public static class ContractsHelper
{
	public static IEnumerable<BeerAvailabilityJson> ToBeerAvailabilities(this IEnumerable<SalesOrderRowJson> rows)
		=> rows.Select(row => new BeerAvailabilityJson(row.BeerId.ToString(), row.BeerName,
			new Availability(row.Quantity.Value, 0, row.Quantity.UnitOfMeasure)));
}