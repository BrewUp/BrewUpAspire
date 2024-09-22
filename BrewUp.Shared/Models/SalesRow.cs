namespace BrewUp.Shared.Models;

public class SalesRow(Guid beerId, string beerName, decimal quantity)
{
    public Guid BeerId { get; } = beerId;
    public string BeerName { get; } = beerName;
    public decimal Quantity { get; } = quantity;
}