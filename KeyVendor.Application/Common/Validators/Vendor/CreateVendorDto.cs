using FluentValidation;
using KeyVendor.Application.Common.Dto.Vendor;

namespace KeyVendor.Application.Common.Validators.Vendor;

public class CreateVendorDtoValidator : AbstractValidator<CreateVendorDto>
{
    public CreateVendorDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}