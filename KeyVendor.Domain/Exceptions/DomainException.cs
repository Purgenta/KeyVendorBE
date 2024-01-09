namespace KeyVendor.Domain.Exceptions;

public class DomainException : BaseException
{
    public DomainException(string message, object? additionalData = null) : base(message, additionalData)
    {
    }

    public DomainException(string message, Exception innerException, object? additionalData = null) : base(message,
        innerException, additionalData)
    {
    }
}