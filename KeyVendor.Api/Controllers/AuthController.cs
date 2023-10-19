using KeyVendor.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

public class AuthController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("BeginLogin")]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command) => Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpGet("{validationToken}/CompleteLogin")]
    public async Task<ActionResult> CompleteLogin([FromRoute] string validationToken) =>
        Ok(await Mediator.Send(new CompleteLoginCommand(validationToken)));
}