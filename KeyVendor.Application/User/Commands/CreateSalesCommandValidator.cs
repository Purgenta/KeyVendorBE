using FluentValidation;
using KeyVendor.Application.Common.Validators.User;

namespace KeyVendor.Application.User.Commands;

public class CreateSalesCommandValidator : AbstractValidator<CreateSalesCommand>
{
    public CreateSalesCommandValidator()
    {
        RuleFor(x => x.User).SetValidator(new CreateUserDtoValidator());
    }
}