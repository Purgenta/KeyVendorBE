using FluentValidation;
using KeyVendor.Application.Common.Dto.Order;

namespace KeyVendor.Application.Common.Validators.Order;

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.Keys).NotNull().NotEmpty().Must(x => x.Count > 0);
    }
}