using KeyVendor.Api.Auth.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace KeyVendor.Api.Auth.Providers;

public class PasswordlessLoginTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public PasswordlessLoginTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<PasswordlessLoginTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider,
        options,
        logger)
    {
    }
}