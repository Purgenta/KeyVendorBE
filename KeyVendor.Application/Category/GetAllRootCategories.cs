using AutoMapper;
using KeyVendor.Application.Common.Dto.Category;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Category;

public record GetAllRootCategoriesQuery : IRequest<List<CategoryDto>>;

public class GetAllRootCategoriesHandler : IRequestHandler<GetAllRootCategoriesQuery, List<CategoryDto>>
{
    private readonly IMapper _mapper;

    public GetAllRootCategoriesHandler(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetAllRootCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await DB.Find<Domain.Entities.Category>().Match(x => x.IsRoot == true)
            .ExecuteAsync(cancellation: cancellationToken);
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}