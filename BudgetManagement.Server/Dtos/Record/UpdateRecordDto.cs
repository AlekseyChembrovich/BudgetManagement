using System.ComponentModel.DataAnnotations;

namespace BudgetManagement.Server.Dtos.Record;

public sealed class UpdateRecordDto
{
    [Required]
    public required Guid Id { get; init; }

    [Required]
    public required decimal Amount { get; init; }

    [Required]
    public required Guid CategoryId { get; init; }
}
