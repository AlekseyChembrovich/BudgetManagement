using System.ComponentModel.DataAnnotations;

namespace BudgetManagement.Server.Dtos.Record;

public sealed class CreateRecordDto
{
    [Required]
    public required decimal Amount { get; init; }

    [Required]
    public required Guid CategoryId { get; init; }
}
