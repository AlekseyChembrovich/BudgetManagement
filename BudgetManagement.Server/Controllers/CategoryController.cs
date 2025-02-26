using AutoMapper;
using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Dtos.Category;
using BudgetManagement.Server.Extensions;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagement.Server.Controllers;

[ApiController]
[Route("categories")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = PoliciesConfiguration.UserPolicy)]
public class CategoryController(
    IExpenseCategoryService expenseCategoryService,
    ITreeBuilder treeBuilder,
    IMapper mapper,
    ILogger<CategoryController> logger)
    : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var category = await expenseCategoryService.GetByIdAsync(id, cancellationToken);
        if (category is null)
            return NotFound();
        
        var subCategories = await expenseCategoryService.GetSubCategoriesAsync(category.Id, cancellationToken);

        var root = mapper.Map<CategoryTreeNodeDto>(category);
        if (subCategories.Count > 0)
        {
            root.Children = mapper.Map<List<CategoryTreeNodeDto>>(subCategories);
        }
        
        return Ok(root);
    }
    
    [HttpGet("list")]
    public async Task<IActionResult> GetCategoryList(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var categories = await expenseCategoryService.GetUserCategoriesAsync(userId, cancellationToken);

        var response = mapper.Map<IEnumerable<CategoryDto>>(categories);

        return Ok(response);
    }
    
    [HttpGet("tree")]
    public async Task<IActionResult> GetCategoryTree(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var categories = await expenseCategoryService.GetUserCategoriesAsync(userId, cancellationToken);

        var response = treeBuilder.BuildTree(categories);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto command, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.Id<Guid>();

        var category = await expenseCategoryService.CreateAsync(command.Name, userId, command.RootId, cancellationToken);

        var response = mapper.Map<CategoryDto>(category);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var category = await expenseCategoryService.DeleteAsync(id, cancellationToken);
        if (category is null)
            return NotFound();

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto command, CancellationToken cancellationToken)
    {
        var category = await expenseCategoryService.UpdateAsync(command.Id, command.Name, cancellationToken);
        if (category is null)
            return NotFound();

        var response = mapper.Map<CategoryDto>(category);

        return Ok(response);
    }
}
