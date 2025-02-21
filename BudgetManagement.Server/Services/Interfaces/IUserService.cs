using BudgetManagement.Core.Entities;

namespace BudgetManagement.Server.Services.Interfaces;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken);
    
    Task<User> CreateAsync(User user, CancellationToken cancellationToken);
}
