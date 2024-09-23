using BrewUp.Shared.Models;
using FluentValidation;

namespace BrewUp.Sales.Facade.Validators;

public class SalesOrderValidator : AbstractValidator<SalesOrderJson>
{
    public SalesOrderValidator()
    {
        RuleFor(v => v.OrderNumber).NotEmpty();
        RuleFor(v => v.CustomerId).NotEqual(Guid.Empty);
        RuleFor(v => v.CustomerName).NotEmpty();

        RuleForEach(v => v.Rows).SetValidator(new SalesOrderLineValidator());
    }
}