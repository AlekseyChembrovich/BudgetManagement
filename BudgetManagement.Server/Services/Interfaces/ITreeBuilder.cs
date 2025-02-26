using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Dtos.Category;

namespace BudgetManagement.Server.Services.Interfaces;

public interface ITreeBuilder
{
    IReadOnlyList<CategoryTreeNodeDto> BuildTree(IReadOnlyList<ExpenseCategory> categories);
}
