namespace BudgetManagement.Server.Dtos.Record;

public class RecordDto
{
    public required Guid Id { get; init; }

    public required decimal Amount { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required Guid CategoryId { get; init; }

    public required Guid UserId { get; init; }
}
