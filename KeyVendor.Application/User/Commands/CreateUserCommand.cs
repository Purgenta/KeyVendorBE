using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Interfaces;
using MediatR;
using KeyVendor.Application.Common.Contants;

namespace KeyVendor.Application.User.Commands;

public record CreateUserCommand(CreateUserDto User) : IRequest;

public class CreateUserHandler : IRequestHandler<CreateUserCommand>
{
    public CreateUserHandler(IUserService iUserService)
    {
        this._userService = iUserService;
    }

    private const double StartingAmount = 1000;
    private readonly IUserService _userService;

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.CreateUserAsync(request.User.Email,
            new List<string> { AuthorizationRoles.Customer }, StartingAmount);
    }
}