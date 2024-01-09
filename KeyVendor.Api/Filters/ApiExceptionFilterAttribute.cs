using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KeyVendor.Api.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleFluentValidationException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (!_exceptionHandlers.TryGetValue(type,
                out var handler))
            return;

        handler.Invoke(context);
    }

    private void HandleFluentValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var failures = exception.Errors
            .GroupBy(e => e.PropertyName,
                e => e.ErrorMessage)
            .ToDictionary(x => x.Key,
                x => x.ToArray());

        var details = new ValidationProblemDetails(failures)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }
}