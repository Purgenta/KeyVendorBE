using KeyVendor.Application.Common.Dto.Key;
using MediatR;

namespace KeyVendor.Application.Key;

public record CreateKeyCommand(CreateKeyDto Data, Domain.Entities.User user) : IRequest;

public class CreateKeyCommandHandler : IRequestHandler<CreateKeyCommand>
{
    public async Task Handle(CreateKeyCommand request, CancellationToken cancellationToken)
    {
        var category = request.Data.CategoryId;
    }
}