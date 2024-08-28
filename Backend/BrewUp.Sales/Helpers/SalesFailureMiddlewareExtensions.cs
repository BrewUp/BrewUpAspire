namespace BrewUp.Sales.Helpers;

public static class SalesFailureMiddlewareExtensions
{
	public static IApplicationBuilder UseSalesFailureMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<SalesFailureMiddleware>();
	}
}