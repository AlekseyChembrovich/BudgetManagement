using BudgetManagement.Core.Entities;

namespace BudgetManagement.Server.Services.Interfaces;

public interface IAuthService
{
    Task<string> GetUserTokenAsync(User user);
}
