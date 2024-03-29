﻿using FluentValidation;
using KeyVendor.Application.Common.Validators.User;

namespace KeyVendor.Application.User.Commands;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User).SetValidator(new CreateUserDtoValidator());
    }
}