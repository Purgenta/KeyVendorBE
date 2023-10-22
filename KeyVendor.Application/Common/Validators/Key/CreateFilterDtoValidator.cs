using FluentValidation;
using KeyVendor.Application.Common.Dto.Key;

namespace KeyVendor.Application.Common.Validators.Key;

public class CreateFilterDtoValidator : AbstractValidator<FilterDto>
{
    public CreateFilterDtoValidator()
    {
        RuleFor(x => x.Page).NotEmpty().Must(x => x > 0);
        RuleFor(x => x.Size).NotEmpty().Must(x => x > 0);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}