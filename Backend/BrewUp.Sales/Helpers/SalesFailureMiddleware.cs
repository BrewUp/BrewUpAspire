namespace BrewUp.Sales.Helpers;

public class SalesFailureMiddleware(RequestDelegate next)
{
	private readonly Random _random = new();

	public Task InvokeAsync(HttpContext context)
	{
		if (_random.NextDouble() >= 0.5)
		{
			throw new Exception("Sales service is not available");
		}

		return next(context);
	}
}