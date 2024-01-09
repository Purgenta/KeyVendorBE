using KeyVendor.Application.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("auth")]
public class AuthController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> BeginLogin(BeginLoginCommand command) => Ok(await Mediator.Send(command));

    [AllowAnonymous]
    [HttpGet("{validationToken}/completelogin")]
    public async Task<ActionResult> CompleteLogin([FromRoute] string validationToken) =>
        Ok(await Mediator.Send(new CompleteLoginCommand(validationToken)));
}