using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Server.Services;

internal sealed class ExpenseCategoryService(DatabaseContext context) : IExpenseCategoryService
{
    public async Task<ExpenseCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return category;
    }

    public async Task<IReadOnlyList<ExpenseCategory>> GetUserCategoriesAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Where(x => x.UserId == null || x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ExpenseCategory>> GetSubCategoriesAsync(Guid rootId, CancellationToken cancellationToken = default)
    {
        return await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .Where(x => x.RootId == rootId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ExpenseCategory> CreateAsync(
        string name,
        Guid userId,
        Guid? rootId = null,
        CancellationToken cancellationToken = default)
    {
        var newCategory = new ExpenseCategory
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            RootId = rootId
        };

        var entry = await context.Set<ExpenseCategory>()
            .AddAsync(newCategory, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }

    public async Task<ExpenseCategory?> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var category = await context.Set<ExpenseCategory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);

        if (category is null)
        {
            return category;
        }

        _ = context.Set<ExpenseCategory>().Remove(category);
        await context.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<ExpenseCategory?> UpdateAsync(Guid categoryId, string name, CancellationToken cancellationToken = default)
    {
        var category = await context.Set<ExpenseCategory>().FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
        if (category is null)
        {
            return category;
        }

        category.Name = name;
        _ = await context.SaveChangesAsync(cancellationToken);

        return category;
    }
}
