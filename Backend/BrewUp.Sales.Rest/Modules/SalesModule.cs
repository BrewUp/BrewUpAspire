using BrewUp.Sales.Messages.Commands;
using BrewUp.Sales.ReadModel.Services;
using BrewUp.Sales.Rest.Validators;
using BrewUp.Sales.SharedKernel.DomainIds;
using BrewUp.Sales.SharedKernel.Dtos;
using BrewUp.Shared.CustomTypes;
using BrewUp.Shared.DomainIds;
using BrewUp.Shared.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Muflone.Persistence;

namespace BrewUp.Sales.Rest.Modules;

public sealed class SalesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;
    
    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<SalesOrderValidator>();
        builder.Services.AddSingleton<ValidationHandler>();
        
        builder.Services.AddScoped<ISalesOrderService, SalesOrderService>();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/sales/")
            .WithTags("Sales");

        group.MapPost("/", HandleCreateOrder)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status201Created)
            .WithName("CreateSalesOrder");
        
        group.MapPut("/", HandleCloseOrder)
            .WithName("CloseSalesOrder");
        
        return endpoints;
    }
    
    private static async Task<IResult> HandleCreateOrder(
        IValidator<SalesOrderJson> validator,
        ValidationHandler validationHandler,
        SalesOrderJson body,
        IServiceBus serviceBus,
        CancellationToken cancellationToken)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);
        
        if (body.OrderId.Equals(Guid.Empty))
            body = body with {OrderId = Guid.NewGuid()};
       
        CreateSalesOrder createSalesOrder = new(new SalesOrderId(body.OrderId),
            new SalesOrderNumber(body.OrderNumber),
            new CustomerId(body.CustomerId), new CustomerName(body.CustomerName), 
            new OrderDate(DateTime.Today), 
            body.Rows.Select(x => new SalesOrderRowDto(
                new BeerId(x.BeerId), new BeerName(x.BeerName),
                new Quantity(x.Quantity, "Lt"), 
                new Price(10, "EUR"))));

        await serviceBus.SendAsync(createSalesOrder, cancellationToken);

        return Results.Created($"/sales/{body.OrderId}", body.OrderId);
    }
    
    private static async Task<IResult> HandleCloseOrder(
        SalesOrderJson body,
        IServiceBus serviceBus,
        CancellationToken cancellationToken)
    {
        CloseSalesOrder closeSalesOrder = new(new SalesOrderId(body.OrderId));

        await serviceBus.SendAsync(closeSalesOrder, cancellationToken);

        return Results.Created($"/sales/{body.OrderId}", body.OrderId);
    }
}