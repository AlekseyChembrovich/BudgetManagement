namespace BudgetManagement.Core.Common;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}
