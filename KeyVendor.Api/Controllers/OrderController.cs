using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Order.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("order")]
public class OrderController : ApiControllerBase
{
    [HttpPost("create")]
    [Authorize(AuthorizationRoles.Sales)]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var email = this.GetUserFromCtx();
        var user = this.UserService.GetUserByEmailAsync(email).Result;
        await this.Mediator.Send(new CreateOrderCommand(createOrderDto, user));
        return Ok();
    }
}