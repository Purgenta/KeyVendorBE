using KeyVendor.Application.User.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("user")]
public class UserController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<ActionResult> CreateUser(CreateUserCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("createsales")]
    public async Task<ActionResult> CreateCustomer(CreateSalesCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }
}