using KeyVendor.Application.Common.Dto.Auth;
using KeyVendor.Application.Common.Interfaces;
using MediatR;

namespace KeyVendor.Application.Auth;

public record CompleteLoginCommand(string ValidationToken) : IRequest<CompleteLoginResponseDto>;

public class CompleteLoginHandler : IRequestHandler<CompleteLoginCommand, CompleteLoginResponseDto>
{
    private readonly IAuthService _authService;

    public CompleteLoginHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<CompleteLoginResponseDto> Handle(CompleteLoginCommand request,
        CancellationToken cancellationToken) => await _authService.CompleteLoginAsync(request.ValidationToken);
}