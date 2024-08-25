using BrewUp.Warehouses.Shared.CustomTypes;

namespace BrewUp.Warehouses.Shared.Contracts;

public record BeerAvailabilityJson(string BeerId, string BeerName, Availability Availability);