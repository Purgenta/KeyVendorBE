using System.Net;
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
    [Authorize]
    public async Task<IActionResult> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        var email = this.GetUserFromCtx();
        var user = this.UserService.GetUserByEmailAsync(email).Result;
        await this.Mediator.Send(new CreateOrderCommand(createOrderDto, user));
        return Ok();
    }

    [HttpPut("update/{id}")]
    [Authorize(AuthorizationRoles.Sales)]
    public async Task<IActionResult> UpdateOrderAsync(UpdateOrderDto updateOrderDto, string id)
    {
        var email = this.GetUserFromCtx();
        var user = this.UserService.GetUserByEmailAsync(email).Result;
        await this.Mediator.Send(new UpdateOrderCommand(id, updateOrderDto, user));
        return Ok();
    }
}