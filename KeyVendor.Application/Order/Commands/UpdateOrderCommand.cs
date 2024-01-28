﻿using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Domain.Entities;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Commands;

public record UpdateOrderCommand(string Id, UpdateOrderDto UpdateOrderDto, Domain.Entities.User User) : IRequest;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
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

        if (request.UpdateOrderDto.OrderStatus == OrderStatus.Completed)
        {
            var buyer = await DB.Find<Domain.Entities.User>().Match(user => user.Email.Equals(order.Buyer.Email))
                .ExecuteSingleAsync(cancellation: cancellationToken);
            if (buyer.Money > order.TotalPrice)
            {
                order.Status = OrderStatus.Completed;

                buyer.Money -= order.TotalPrice;
                await buyer.SaveAsync(cancellation: cancellationToken);
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