namespace BudgetManagement.Server.Dtos.Report;

public class ReportDto
{
    public required Guid CategoryId { get; init; }
    
    public required string CategoryName { get; init; }
    
    public required decimal TotalSum { get; init; }
}
