using BudgetManagement.Core.Common;

namespace BudgetManagement.Core.Entities;

public record class ExpenseCategory : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public Guid? RootId { get; set; }

    public ExpenseCategory? Root { get; set; }
}
