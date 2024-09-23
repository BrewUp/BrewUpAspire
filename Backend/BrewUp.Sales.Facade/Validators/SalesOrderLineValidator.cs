using BrewUp.Shared.Models;
using FluentValidation;

namespace BrewUp.Sales.Facade.Validators;

public class SalesOrderLineValidator : AbstractValidator<SalesRow>
{
    public SalesOrderLineValidator()
    {
        RuleFor(v => v.BeerId).NotEqual(Guid.Empty);
        RuleFor(v => v.BeerName).NotEmpty();

        RuleFor(v => v.Quantity).GreaterThan(0);
    }
}