namespace KeyVendor.Infrastructure.Exceptions;

internal class AuthException : InfrastructureException
{
    public AuthException(string message, object? additionalData = null) : base(message,
        additionalData)
    {
    }

    public AuthException(string message, Exception innerException, object? additionalData = null) : base(message,
        innerException,
        additionalData)
    {
    }
}