using KeyVendor.Application.Category;
using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Key.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeyVendor.Api.Controllers;

[Route("category")]
public class CategoryController : ApiControllerBase
{
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<ActionResult> CreateCategory(CreateCategoryCommand categoryCommand)
    {
        await Mediator.Send(categoryCommand);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<ActionResult> AllLeafCategories()
    {
        var categories = await Mediator.Send(new GetAllCategoriesQuery());
        return Ok(categories);
    }

    [HttpGet("rootcategories")]
    public async Task<ActionResult> AllRootCategories()
    {
        var categories = await Mediator.Send(new GetAllRootCategoriesQuery());
        return Ok(categories);
    }
}