using KeyVendor.Application.Common.Dto.Vendor;
using KeyVendor.Application.Vendor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KeyVendor.Api.Controllers;

public class VendorController : ApiControllerBase
{
    [HttpPost("create")]
    [Authorize]
    public async void CreateVendor(CreateVendorDto createVendor)
    {
        var user = await UserService.GetUserByEmailAsync(this.GetUserFromCtx());
        await Mediator.Send(new CreateVendorCommand(createVendor, user!));
        Ok();
    }
}