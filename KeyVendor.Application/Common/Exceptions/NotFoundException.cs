namespace KeyVendor.Application.Common.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message, object? additionalData = null) : base(message,
        additionalData)
    {
    }

    public NotFoundException(string message, Exception innerException, object? additionalData = null) : base(message,
        innerException,
        additionalData)
    {
    }
}