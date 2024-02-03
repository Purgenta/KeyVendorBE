using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Order.Commands;
using KeyVendor.Application.Order.Query;
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

    [Authorize]
    [HttpGet("findbybuyer")]
    public async Task<IActionResult> GetOrdersByUser(FilterByUser request)
    {
        var email = this.GetUserFromCtx();
        var user = this.UserService.GetUserByEmailAsync(email).Result;
        var filter = new FilterOrderDto(user.Email, null, request.Status, request.Page, request.Size);
        return Ok(await this.Mediator.Send(filter));
    }

    [Authorize(AuthorizationRoles.Director)]
    [HttpGet("overview")]
    public async Task<IActionResult> GetOrdersOverview([FromQuery] OverviewOrderDto filter)
    {
        return Ok(await this.Mediator.Send(filter));
    }

    [AllowAnonymous]
    [HttpGet("status/options")]
    public async Task<IActionResult> GetOrderStatusOptions()
    {
        return Ok(await this.Mediator.Send(new OrderOptionsQuery()));
    }
}