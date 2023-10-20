using FluentValidation;
using KeyVendor.Application.Common.Dto.Key;

namespace KeyVendor.Application.Common.Validators.Key;

public class CreateKeyDtoValidator : AbstractValidator<CreateKeyDto>
{
    public CreateKeyDtoValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty();
        RuleFor(x => x.ValidUntil).Must(BeAValidDate).WithMessage("Date is invalid (has to be in future)");
        RuleFor(x => x.CategoryId).NotEmpty();
    }

    private static bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}