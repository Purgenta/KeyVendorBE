using KeyVendor.Application.Common.Dto.Category;
using KeyVendor.Domain.Entities;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Category;

public record CreateCategoryCommand(CreateCategoryDto CategoryDto) : IRequest;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
{
    public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var parent = await DB.Find<Domain.Entities.Category>()
            .Match(category => category.ID == request.CategoryDto.ParentId)
            .ExecuteSingleAsync(cancellation: cancellationToken);
        var createdCategory = new Domain.Entities.Category();
        createdCategory.Name = request.CategoryDto.Name;
        await createdCategory.SaveAsync(cancellation: cancellationToken);
        if (parent != null)
        {
            parent.ChildCategories.Add(new ChildCategory(createdCategory.Name, createdCategory.ID));
            await parent.SaveAsync(cancellation: cancellationToken);
        }
    }
}