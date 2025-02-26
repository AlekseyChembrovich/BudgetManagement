using System.ComponentModel.DataAnnotations;

namespace BudgetManagement.Server.Dtos.Category;

public sealed class CreateCategoryDto
{
    [Required]
    [MaxLength(300)]
    public required string Name { get; init; }

    public Guid? RootId { get; init; }
}
