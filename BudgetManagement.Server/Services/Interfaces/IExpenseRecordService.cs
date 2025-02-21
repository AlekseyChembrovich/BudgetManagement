using BudgetManagement.Core.Entities;

namespace BudgetManagement.Server.Services.Interfaces;

public interface IExpenseRecordService
{
    Task<IReadOnlyList<ExpenseRecord>> GetUserRecordsAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<ExpenseRecord> CreateAsync(decimal amount, Guid categoryId, Guid userId, CancellationToken cancellationToken = default);

    Task<ExpenseRecord?> DeleteAsync(Guid recordId, CancellationToken cancellationToken = default);

    Task<ExpenseRecord?> UpdateAsync(Guid recordId, decimal amount, Guid categoryId, CancellationToken cancellationToken = default);
}
