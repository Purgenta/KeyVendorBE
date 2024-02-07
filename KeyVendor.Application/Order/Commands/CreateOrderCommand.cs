using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Common.Interfaces;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Commands;

public record CreateOrderCommand(CreateOrderDto CreateOrderDto, Domain.Entities.User user) : IRequest;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IUserService _userService;

    public CreateOrderCommandHandler(IUserService userService)
    {
        this._userService = userService;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = request.user;
        var order = request.CreateOrderDto;
        var keys = await DB.Find<Domain.Entities.Key>().Match(key => order.Keys.Contains(key.ID))
            .ExecuteAsync(cancellation: cancellationToken);

        var totalPrice = keys.Sum(key => key.Price + (key.Tax * key.Price / 100));
        if (user.Money < totalPrice)
            throw new Exception("Not enough money");
        if (!keys.All(key => key.CreatedBy.Email.Equals(keys.First().CreatedBy.Email)))
        {
            throw new Exception("Keys are not from the same seller");
        }

        if (keys.Count != 1 && !keys.All(key => key.Name != keys.First().Name && key.LicensedFor.Count != 0))
        {
            throw new Exception("Keys must have the same name to be eligible for group buy licencing");
        }

        var sellerEmail = keys[0].CreatedBy.Email;
        var seller = await this._userService.GetUserByEmailAsync(sellerEmail);
        if (seller == null) throw new Exception("Seller not found");
        var createdOrder = new Domain.Entities.Order(totalPrice, user, seller);
        createdOrder.Keys = keys;
        await createdOrder.SaveAsync(cancellation: cancellationToken);
    }
}