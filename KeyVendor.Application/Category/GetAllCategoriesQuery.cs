using AutoMapper;
using KeyVendor.Application.Common.Dto.Category;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Category;

public record GetAllCategoriesQuery : IRequest<List<CategoryDto>>;

public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryDto>>
{
    private readonly IMapper _mapper;

    public GetAllCategoriesHandler(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await DB.Find<Domain.Entities.Category>().Match(x => x.Active == true)
            .Match(x => x.ChildCategories.Count == 0).ExecuteAsync(cancellation: cancellationToken);
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}