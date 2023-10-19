using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto.Key;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("key")]
public class KeyController : ApiControllerBase
{
    [HttpPost("create")]
    [Authorize(AuthorizationRoles.Sales)]
    public async void CreateKey(CreateKeyDto key)
    {
        var email = this.GetUserFromCtx();
        var user = await this.UserService.GetUserByEmailAsync(email);
        await Mediator.Send(key);
        Ok();
    }
}