using BudgetManagement.Core.Common;

namespace BudgetManagement.Core.Entities;

public record class User : IEntity<Guid>
{
    public Guid Id { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
