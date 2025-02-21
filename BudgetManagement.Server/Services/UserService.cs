using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Server.Services;

internal sealed class UserService(DatabaseContext context) : IUserService
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return context.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
    }

    public Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken)
    {
        return context.Users.AsNoTracking()
            .FirstOrDefaultAsync(user => user.Login == login, cancellationToken);
    }

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
    {
        var entry = await context.Users.AddAsync(user, cancellationToken);

        _ = await context.SaveChangesAsync(cancellationToken);

        return entry.Entity;
    }
}
