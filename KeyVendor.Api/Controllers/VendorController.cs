using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto.Vendor;
using KeyVendor.Application.Vendor;
using KeyVendor.Application.Vendor.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace KeyVendor.Api.Controllers;

[Route("vendor")]
public class VendorController : ApiControllerBase
{
    [HttpPost("create")]
    [Authorize]
    public async Task CreateVendor(CreateVendorDto createVendor)
    {
        var user = await UserService.GetUserByEmailAsync(this.GetUserFromCtx());
        await Mediator.Send(new CreateVendorCommand(createVendor, user!));
        Ok();
    }

    [HttpGet("allvendors")]
    public async Task<ActionResult> GetAllVendors()
    {
        var result = await Mediator.Send(new GetAllVendorQuery());
        return Ok(result);
    }
}