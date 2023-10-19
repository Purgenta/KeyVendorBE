using KeyVendor.Application.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Api.Controllers;

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
}