using KeyVendor.Application.Common.Contants;
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

    [HttpGet("photo/{id}")]
    public IActionResult GetKeyImage(string id)
    {
        var image = Mediator.Send(new GetKeyQuery(id));
        return File(image.Result, "image/jpeg");
    }


    [HttpGet("{id}")]
    public async Task<ActionResult> GetKey(string id)
    {
        return Ok(await Mediator.Send(new GetKeyById(id)));
    }

    [HttpPut("update/{id}")]
    [Consumes("multipart/form-data")]
    [Authorize]
    public async Task<ActionResult> UpdateKey([FromForm] CreateKeyDto key, string id)
    {
        return Ok(await Mediator.Send(new GetKeyById(id)));
    }

    [HttpGet("find")]
    public async Task<ActionResult> FilteredKeys([FromQuery] FilterDto filterDto)
    {
        return Ok(await Mediator.Send(new GetFilteredKey(filterDto)));
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = AuthorizationRoles.Sales)]
    public async Task<ActionResult> DeleteKey([FromRoute] string id)
    {
        var email = this.GetUserFromCtx();
        var user = await this.UserService.GetUserByEmailAsync(email);
        await Mediator.Send(new DeleteKeyCommand(id, user!));
        return Ok();
    }
}