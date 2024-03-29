﻿using FluentValidation;
using KeyVendor.Application.Common.Dto.Key;
using Microsoft.AspNetCore.Http;

namespace KeyVendor.Application.Common.Validators.Key;

public class CreateKeyDtoValidator : AbstractValidator<CreateKeyDto>
{
    public CreateKeyDtoValidator()
    {
        RuleFor(x => x.Value)
            .NotEmpty();
        RuleFor(x => x.ValidUntil).NotEmpty().NotNull().Must(BeAValidDate)
            .WithMessage("Date is invalid (has to be in future)");
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Photo).NotEmpty().NotNull().Must(BeValidPhoto).WithMessage("Image must be of type image/jpeg");
        RuleFor(x => x.Price).NotEmpty().Must(ValidPrice).WithMessage("Minimum price is 0.1");
        RuleFor(x => x.Tax).NotNull().Must(ValidPrice).WithMessage("Tax can be 0% or higher");
    }

    private static bool BeValidPhoto(IFormFile photo)
    {
        return photo.ContentType == "image/jpeg";
    }

    private static bool ValidPrice(double price)
    {
        return price > 0.1;
    }

    private static bool ValidTax(double tax)
    {
        return tax >= 0.0;
    }

    private static bool BeAValidDate(string date)
    {
        try
        {
            var time = DateTime.Parse(date);
            if (time.Date > new DateTime().Date)
            {
                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}