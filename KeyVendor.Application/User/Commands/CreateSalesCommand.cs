using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Interfaces;
using MediatR;

namespace KeyVendor.Application.User.Commands;

public record CreateSalesCommand(CreateUserDto User) : IRequest;

public class CreateSalesHandler : IRequestHandler<CreateSalesCommand>
{
    public CreateSalesHandler(IUserService iUserService)
    {
        this._userService = iUserService;
    }

    private const double StartingAmount = 1000;
    private readonly IUserService _userService;

    public async Task Handle(CreateSalesCommand request, CancellationToken cancellationToken)
    {
        await _userService.CreateUserAsync(request.User.Email,
            new List<string> { AuthorizationRoles.Sales }, StartingAmount);
    }
}