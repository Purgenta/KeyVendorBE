namespace KeyVendor.Domain.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message, object? additionalData = null) : base(message)
    {
        AdditionalData = additionalData;
    }

    public BaseException(string message, Exception innerException, object? additionalData = null) : base(message,
        innerException)
    {
        AdditionalData = additionalData;
    }

    public object? AdditionalData { get; }
}