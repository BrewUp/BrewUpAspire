using BrewUp.Shared.Models;
using Microsoft.AspNetCore.Components;
using ResilienceBlazor.Modules.Sales.Extensions;

namespace ResilienceBlazor.Modules.Sales;

public class SalesBase : ComponentBase, IDisposable
{
	[Inject] private ISalesService SalesService { get; set; } = default!;

	protected IQueryable<SalesOrder> SalesOrders { get; set; } = default!;
	protected string ErrorMessage { get; set; } = string.Empty;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
	}

	protected async Task CreateSalesOrderAsync()
	{
		try
		{
			SalesOrder salesOrder = new(
				Guid.NewGuid(),
				Guid.NewGuid().ToString(),
				Guid.NewGuid(),
				"Il Grottino del Muflone",
				0,
				"EUR",
				Enumerable.Empty<SalesRow>()
			);
			var result = await SalesService.CreateSalesOrderAsync(salesOrder, CancellationToken.None);

			Thread.Sleep(500);

			await GetSalesOrdersAsync();
		}
		catch (Exception ex)
		{
			ErrorMessage = ex.Message;
		}

		StateHasChanged();
	}

	protected async Task GetSalesOrdersAsync()
	{
		try
		{
			var result = await SalesService.GetSalesOrdersAsync(CancellationToken.None);
			SalesOrders = result.AsQueryable();

			ErrorMessage = "Success";
		}
		catch (Exception ex)
		{
			SalesOrders = new List<SalesOrder>().AsQueryable();
			ErrorMessage = ex.Message;
		}

		StateHasChanged();
	}

	#region Dispose
	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
		}
	}
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~SalesBase()
	{
		Dispose(false);
	}
	#endregion
}