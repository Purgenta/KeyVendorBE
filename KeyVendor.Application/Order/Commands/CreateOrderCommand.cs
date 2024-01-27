using KeyVendor.Application.Common.Dto.Order;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Commands;

public record CreateOrderCommand(CreateOrderDto CreateOrderDto, Domain.Entities.User user) : IRequest;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
{
    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = request.user;
        var order = request.CreateOrderDto;
        var keys = await DB.Find<Domain.Entities.Key>().Match(key => order.Keys.Contains(key.ID))
            .ExecuteAsync(cancellation: cancellationToken);

        var totalPrice = keys.Sum(key => key.Price + (key.Tax * key.Price / 100));
        if (user.Money < totalPrice)
            throw new Exception("Not enough money");
        if (!keys.All(key => key.CreatedBy.ID.Equals(keys.First().CreatedBy.ID)))
        {
            throw new Exception("Keys are not from the same seller");
        }

        if (keys.Count != 1 && !keys.All(key => key.Name != keys.First().Name && key.LicensedFor.Count != 0))
        {
            throw new Exception("Keys must have the same name to be eligible for group buy licencing");
        }

        var seller = await DB.Find<Domain.Entities.User>()
            .Match(user => user.Email.Equals(keys.First().CreatedBy.ID))
            .ExecuteSingleAsync(cancellation: cancellationToken);
        var createdOrder = new Domain.Entities.Order(totalPrice, new(user.ID),
            new Many<Domain.Entities.Key>(), new(seller.ID));
        await createdOrder.SaveAsync(cancellation: cancellationToken);
        await createdOrder.Keys.AddAsync(keys, cancellation: cancellationToken);
    }
}