using FluentValidation.Results;

namespace KeyVendor.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static IDictionary<string, string[]> ToGroup(this List<ValidationFailure> validationFailures)
    {
        return validationFailures.GroupBy(e => e.PropertyName,
                e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key,
                failureGroup => failureGroup.ToArray());
    }
}