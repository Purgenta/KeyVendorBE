using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Key;
using KeyVendor.Application.Key.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("key")]
public class KeyController : ApiControllerBase
{
    [HttpPost("create")]
    [Consumes("multipart/form-data")]
    [Authorize]
    public async Task CreateKey([FromForm] CreateKeyDto key)
    {
        var email = this.GetUserFromCtx();
        var user = await this.UserService.GetUserByEmailAsync(email);
        await Mediator.Send(new CreateKeyCommand(key, user!));
        Ok();
    }

    [HttpGet("photo/${id}")]
    public IActionResult GetKeyImage(string id)
    {
        var image = Mediator.Send(new GetKeyQuery(id));
        return File(image.Result, "image/jpeg");
    }
}