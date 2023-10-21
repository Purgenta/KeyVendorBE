using KeyVendor.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Application.Key.Query;

public record GetKeyQuery(string Id) : IRequest<FileStream>;

public class GetKeyQueryHandler : IRequestHandler<GetKeyQuery, FileStream>
{
    private readonly IFileUploadService _fileUploadService;

    public GetKeyQueryHandler(IFileUploadService fileUploadService)
    {
        this._fileUploadService = fileUploadService;
    }

    public async Task<FileStream> Handle(GetKeyQuery request, CancellationToken cancellationToken)
    {
        return this._fileUploadService.GetImage(request.Id);
    }
}