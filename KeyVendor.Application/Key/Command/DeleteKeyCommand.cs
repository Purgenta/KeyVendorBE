using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Key;

public record DeleteKeyCommand(string Id, Domain.Entities.User User) : IRequest;

public class DeleteKeyCommandHandler : IRequestHandler<DeleteKeyCommand>
{
    public async Task Handle(DeleteKeyCommand request, CancellationToken cancellationToken)
    {
        var key = await DB.Find<Domain.Entities.Key>().OneAsync(request.Id, cancellation: cancellationToken);
        var res = await DB.Update<Domain.Entities.Key>().MatchID(request.Id)
            .Match(x => x.CreatedBy.Email == request.User.Email)
            .Modify(x => x.Active, false)
            .ExecuteAsync(cancellation: cancellationToken);
        if (res.MatchedCount == 0) throw new Exception("No keys matched the description found");
    }
}