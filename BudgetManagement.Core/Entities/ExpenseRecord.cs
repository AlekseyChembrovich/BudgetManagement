using BudgetManagement.Core.Common;

namespace BudgetManagement.Core.Entities;

public record class ExpenseRecord : IEntity<Guid>
{
    public Guid Id { get; set; }
    
    public decimal Amount { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public ExpenseCategory? Category { get; set; }
    
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
}
