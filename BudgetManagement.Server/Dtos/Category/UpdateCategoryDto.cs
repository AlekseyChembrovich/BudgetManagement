using System.ComponentModel.DataAnnotations;

namespace BudgetManagement.Server.Dtos.Category;

public sealed class UpdateCategoryDto
{
    [Required]
    public required Guid Id { get; init; }

    [Required]
    [MaxLength(300)]
    public required string Name { get; init; }
}
