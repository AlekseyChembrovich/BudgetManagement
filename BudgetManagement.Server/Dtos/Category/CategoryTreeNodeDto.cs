namespace BudgetManagement.Server.Dtos.Category;

public sealed class CategoryTreeNodeDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public List<CategoryTreeNodeDto>? Children { get; set; }
    
    public required bool Deletable { get; init; }
}
