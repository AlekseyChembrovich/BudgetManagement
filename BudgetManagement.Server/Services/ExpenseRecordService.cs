using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Server.Services;

internal sealed class ExpenseRecordService(DatabaseContext context) : IExpenseRecordService
{
    public async Task<IReadOnlyList<ExpenseRecord>> GetUserRecordsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ExpenseRecord>()
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ExpenseRecord>> GetUserRecordsAsync(
        Guid userId,
        Guid? categoryId,
        DateTime? from,
        DateTime? to,
        CancellationToken cancellationToken = default)
    {
        var categoryQuery = context.Set<ExpenseCategory>().AsNoTracking();
        categoryQuery = categoryId.HasValue
            ? categoryQuery.Where(x => x.RootId == categoryId.Value)
            : categoryQuery.Where(x => x.RootId == null);
        
        var categories = await categoryQuery.ToListAsync(cancellationToken);
        var categoryIds = categories.Select(x => x.Id).ToArray();
        
        var recordQuery = context.Set<ExpenseRecord>()
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.UserId == userId);

        if (from.HasValue && to.HasValue)
        {
            from = new DateTime(from.Value.Year, from.Value.Month, from.Value.Day, 0, 0, 0, DateTimeKind.Utc);
            to = new DateTime(to.Value.Year, to.Value.Month, to.Value.Day, 0, 0, 0, DateTimeKind.Utc);

            recordQuery = recordQuery.Where(x => x.CreatedAt >= from && x.CreatedAt <= to);
        }
        
        var records = await recordQuery
            .Where(x => categoryIds.Contains(x.CategoryId))
            .ToListAsync(cancellationToken);
        
        return records;
    }

    public async Task<ExpenseRecord> CreateAsync(
        decimal amount,
        Guid categoryId,
        Guid userId,
        CancellationToken cancellationToken = default)
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
        
        var category = await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        
        entry.Entity.Category = category;
        
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
            .FirstOrDefaultAsync(x => x.Id == recordId, cancellationToken);

        if (record is null)
        {
            return record;
        }

        record.Amount = amount;
        record.CategoryId = categoryId;
        _ = await context.SaveChangesAsync(cancellationToken);
        
        var category = await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        
        record.Category = category;

        return record;
    }
}
