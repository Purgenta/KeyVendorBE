using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Common.Interfaces;
using KeyVendor.Domain.Entities;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Commands;

public record UpdateOrderCommand(string Id, UpdateOrderDto UpdateOrderDto, Domain.Entities.User User) : IRequest;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IUserService _userService;

    public UpdateOrderCommandHandler(IUserService userService)
    {
        this._userService = userService;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;
        var order = await DB.Find<Domain.Entities.Order>().Match(order => order.ID == request.Id)
            .ExecuteSingleAsync(cancellation: cancellationToken);
        if (order == null)
        {
            throw new Exception("Order not found");
        }

        if (order.Seller.ID.Equals(user.ID) == false)
        {
            throw new Exception("You are not the seller of this order");
        }

        if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
        {
            throw new Exception("Order is already handled");
        }

        if (request.UpdateOrderDto.Status == "Completed")
        {
            var buyer = await this._userService.GetUserByEmailAsync(order.Buyer.Email);
            if (buyer.Money > order.TotalPrice)
            {
                order.Status = OrderStatus.Completed;
                var keyIds = order.Keys.Select(key => key.ID).ToList();

                var money = buyer.Money - order.TotalPrice;
                await _userService.UpdateUserMoneyAsync(user.Email, money);
                await DB.Update<Domain.Entities.Key>().Match(key => keyIds.Contains(key.ID))
                    .Modify(set => set.Active, false).ExecuteAsync(cancellation: cancellationToken);
            }
            else order.Status = OrderStatus.Cancelled;
        }
        else
        {
            order.Status = OrderStatus.Cancelled;
        }

        await order.SaveAsync(cancellation: cancellationToken);
    }
}