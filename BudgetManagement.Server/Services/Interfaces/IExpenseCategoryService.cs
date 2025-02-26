using BudgetManagement.Core.Entities;

namespace BudgetManagement.Server.Services.Interfaces;

public interface IExpenseCategoryService
{
    Task<ExpenseCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<IReadOnlyList<ExpenseCategory>> GetUserCategoriesAsync(Guid userId, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<ExpenseCategory>> GetSubCategoriesAsync(Guid rootId, CancellationToken cancellationToken = default);

    Task<ExpenseCategory> CreateAsync(string name, Guid userId, Guid? rootId = null, CancellationToken cancellationToken = default);

    Task<ExpenseCategory?> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);

    Task<ExpenseCategory?> UpdateAsync(Guid categoryId, string name, CancellationToken cancellationToken = default);
}
