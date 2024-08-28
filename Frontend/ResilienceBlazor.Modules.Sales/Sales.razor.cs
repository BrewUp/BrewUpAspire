using BrewUp.Shared.Models;
using Microsoft.AspNetCore.Components;
using ResilienceBlazor.Modules.Sales.Extensions;

namespace ResilienceBlazor.Modules.Sales;

public class SalesBase : ComponentBase, IDisposable
{
	[Inject] private ISalesService SalesService { get; set; } = default!;

	protected IQueryable<SalesOrder> SalesOrders { get; set; } = default!;
	protected string ErrorMessage { get; set; } = string.Empty;

	protected bool WaitErrorReset;
	protected bool HideResponse = true;

	protected int GoodResponses = 0;
	protected int BadResponses = 0;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();
	}

	protected async Task GetSalesOrdersAsync()
	{
		try
		{
			var result = await SalesService.GetSalesOrdersAsync(CancellationToken.None);
			SalesOrders = result.AsQueryable();

			ErrorMessage = "Success";
			HideResponse = true;

			GoodResponses++;
		}
		catch (Exception ex)
		{
			SalesOrders = new List<SalesOrder>().AsQueryable();
			ErrorMessage = ex.Message;
			WaitErrorReset = true;
			HideResponse = false;

			BadResponses++;
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