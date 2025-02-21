namespace BudgetManagement.Server.Dtos.Category;

public sealed class CategoryDto
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public Guid? UserId { get; init; }

    public Guid? RootId { get; init; }
}
