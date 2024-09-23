using BrewUp.Sales.Facade.Validators;
using BrewUp.Shared.Models;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace BrewUp.Sales.Facade.Endpoints;

public static class SalesEndpoints
{
    public static IEndpointRouteBuilder MapSalesEndpoints(this IEndpointRouteBuilder endpoints) 
    {
        var group = endpoints.MapGroup("/v1/sales/")
            .WithTags("Sales");

        group.MapPost("/", HandleCreateOrder)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithName("CreateSalesOrder");

        return endpoints;
    }

    private static async Task<IResult> HandleCreateOrder(
        ISalesFacade salesFacade,
        IValidator<SalesOrderJson> validator,
        ValidationHandler validationHandler,
        SalesOrderJson body,
        CancellationToken cancellationToken)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        var orderId = await salesFacade.CreateOrderAsync(body, cancellationToken);

        return Results.Created($"/v1/sales/orders/{orderId}", orderId);
    }
}