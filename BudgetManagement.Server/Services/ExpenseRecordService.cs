using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Server.Services;

public class ExpenseRecordService(DatabaseContext context) : IExpenseRecordService
{
    public async Task<IReadOnlyList<ExpenseRecord>> GetUserRecordsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ExpenseRecord>()
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ExpenseRecord> CreateAsync(decimal amount, Guid categoryId, Guid userId, CancellationToken cancellationToken = default)
    {
        var newRecord = new ExpenseRecord
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            CreatedAt = DateTime.UtcNow,
            CategoryId = categoryId,
            UserId = userId
        };

        var entry = await context.Set<ExpenseRecord>()
            .AddAsync(newRecord, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<ExpenseRecord?> DeleteAsync(Guid recordId, CancellationToken cancellationToken = default)
    {
        var record = await context.Set<ExpenseRecord>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == recordId, cancellationToken);

        if (record is null)
        {
            return record;
        }

        _ = context.Set<ExpenseRecord>().Remove(record);
        await context.SaveChangesAsync(cancellationToken);

        return record;
    }

    public async Task<ExpenseRecord?> UpdateAsync(Guid recordId, decimal amount, Guid categoryId, CancellationToken cancellationToken = default)
    {
        var record = await context.Set<ExpenseRecord>()
            .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);

        if (record is null)
        {
            return record;
        }

        record.Amount = amount;
        record.CategoryId = categoryId;
        _ = await context.SaveChangesAsync(cancellationToken);

        return record;
    }
}
