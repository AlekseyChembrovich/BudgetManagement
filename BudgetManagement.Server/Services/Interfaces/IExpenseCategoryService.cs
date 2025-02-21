using BudgetManagement.Core.Entities;

namespace BudgetManagement.Server.Services.Interfaces;

public interface IExpenseCategoryService
{
    Task<IReadOnlyList<ExpenseCategory>> GetUserCategoriesAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<ExpenseCategory> CreateAsync(string name, Guid userId, CancellationToken cancellationToken = default);

    Task<ExpenseCategory?> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);

    Task<ExpenseCategory?> UpdateAsync(Guid categoryId, string name, CancellationToken cancellationToken = default);
}
