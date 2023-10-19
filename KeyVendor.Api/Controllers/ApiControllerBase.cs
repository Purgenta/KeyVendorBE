using KeyVendor.Application.Common.Interfaces;
using KeyVendor.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    private IUserService? _userService;

    protected string GetUserFromCtx()
    {
        var email = HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
        return email!;
    }

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IUserService UserService =>
        _userService ??= HttpContext.RequestServices.GetRequiredService<IUserService>();
}