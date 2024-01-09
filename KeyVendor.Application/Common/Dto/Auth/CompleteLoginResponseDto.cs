namespace KeyVendor.Application.Common.Dto.Auth;

public record CompleteLoginResponseDto(string? EmailAddress = null, List<string>? Roles = null,
    string? JwtToken = null);