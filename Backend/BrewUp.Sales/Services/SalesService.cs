using BrewUp.Sales.Domain;
using BrewUp.Sales.Shared;
using BrewUp.Shared.Commands;
using BrewUp.Shared.Models;

namespace BrewUp.Sales.Services;

public class SalesService(AzureServiceBusConfiguration azureServiceBusConfiguration,
	AzureServiceBus serviceBus)
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

	public async Task<string> CreateSalesOrderAsync()
	{
		SalesOrder salesOrder = new(
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
		);
		
		CreateSalesOrder salesOrderCommand = new (salesOrder.OrderId, salesOrder.OrderNumber,
			salesOrder.CustomerId, salesOrder.CustomerName,
			salesOrder.TotalAmount, salesOrder.Currency,
			salesOrder.Rows);
		
		var command = await BrewUpSerializer.SerializeAsync(salesOrderCommand);
		await serviceBus.SendAsync(command).ConfigureAwait(false);
		
		return await Task.FromResult(salesOrder.ToString());
	}
}