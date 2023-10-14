using FluentValidation;
using KeyVendor.Application.Common.Dto;

namespace KeyVendor.Application.Common.Validators.User;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}