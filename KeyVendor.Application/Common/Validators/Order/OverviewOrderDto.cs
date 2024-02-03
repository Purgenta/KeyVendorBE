using FluentValidation;
using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Domain.Entities;

namespace KeyVendor.Application.Common.Validators.Order;

public class OverviewOrderDtoValidator : AbstractValidator<OverviewOrderDto>
{
    public OverviewOrderDtoValidator()
    {
        RuleFor(x => x.Status).NotNull().NotEmpty().Must(x => Enum.IsDefined(typeof(OrderStatus), x));
        RuleFor(x => x.StartDate).NotNull().NotEmpty().LessThan(x => x.EndDate);
        RuleFor(x => x.EndDate).NotNull().NotEmpty().GreaterThan(x => x.StartDate);
    }
}