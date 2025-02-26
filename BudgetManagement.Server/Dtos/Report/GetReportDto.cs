namespace BudgetManagement.Server.Dtos.Report;

public class GetReportDto
{
    public Guid? CategoryId { get; set; }
    
    public DateTime? From { get; set; }
    
    public DateTime? To { get; set; }
}
