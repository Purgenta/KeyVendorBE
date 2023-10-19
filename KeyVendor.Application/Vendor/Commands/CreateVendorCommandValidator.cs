using FluentValidation;
using KeyVendor.Application.Common.Validators.Vendor;

namespace KeyVendor.Application.Vendor.Commands;

public class CreateVendorCommandValidator : AbstractValidator<CreateVendorCommand>
{
    public CreateVendorCommandValidator()
    {
        RuleFor(x => x.Req).SetValidator(new CreateVendorDtoValidator());
    }
}