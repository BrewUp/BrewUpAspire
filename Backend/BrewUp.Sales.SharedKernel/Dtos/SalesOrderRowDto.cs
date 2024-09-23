using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;

namespace BrewUp.Sales.SharedKernel.Dtos;

public record SalesOrderRowDto(BeerId BeerId, BeerName BeerName, Quantity Quantity, Price Price);