using KeyVendor.Application.Common.Dto.Auth;
using KeyVendor.Application.Common.Interfaces;
using MediatR;

namespace KeyVendor.Application.Auth;

public record BeginLoginCommand(string EmailAddress) : IRequest<BeginLoginResponseDto>;

public class BeginLoginCommandHandler : IRequestHandler<BeginLoginCommand, BeginLoginResponseDto>
{
    private readonly IAuthService _authService;

    public BeginLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }


    public async Task<BeginLoginResponseDto> Handle(BeginLoginCommand request, CancellationToken cancellationToken)
    {
        return await _authService.BeginLoginAsync(request.EmailAddress);
    }
}