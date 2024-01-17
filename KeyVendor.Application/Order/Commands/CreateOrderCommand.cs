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
        var totalPrice = keys.Sum(key => key.Price);
        if (user.Money < totalPrice)
            throw new Exception("Not enough money");
        if (!keys.All(key => key.CreatedBy.Email.Equals(keys.First().CreatedBy.Email)))
        {
            throw new Exception("Keys are not from the same seller");
        }

        var seller = await DB.Find<Domain.Entities.User>()
            .Match(user => user.Email.Equals(keys.First().CreatedBy.Email))
            .ExecuteSingleAsync(cancellation: cancellationToken);
        var createdOrder = new Domain.Entities.Order(totalPrice, new One<Domain.Entities.User>(user),
            new Many<Domain.Entities.Key>(), new One<Domain.Entities.User>(seller));
        await createdOrder.SaveAsync(cancellation: cancellationToken);
        await createdOrder.Keys.AddAsync(keys, cancellation: cancellationToken);
    }
}