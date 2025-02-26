using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Dtos.Category;
using BudgetManagement.Server.Services.Interfaces;

namespace BudgetManagement.Server.Services;

internal sealed class TreeBuilder : ITreeBuilder
{
    public IReadOnlyList<CategoryTreeNodeDto> BuildTree(IReadOnlyList<ExpenseCategory> categories)
        => categories.Where(x => x.RootId == null)
            .OrderBy(x => x.Name)
            .Select(rootCategory => BuildRoot(rootCategory, categories))
            .ToList();
    
    private static CategoryTreeNodeDto BuildRoot(ExpenseCategory root, IReadOnlyList<ExpenseCategory> categories)
    {
        var rootNode = new CategoryTreeNodeDto
        {
            Id = root.Id,
            Name = root.Name,
            Children = null,
            Deletable = root.UserId is not null
        };

        var subCategories = categories.Where(x => x.RootId == root.Id)
            .OrderBy(x => x.Name)
            .ToList();
        
        if (subCategories.Count != 0)
        {
            rootNode.Children = [];
        }

        foreach (var subCategory in subCategories)
        {
            var isRoot = categories.Any(x => x.RootId == subCategory.Id);
            if (isRoot)
            {
                var subRoot = BuildRoot(subCategory, categories);
                rootNode.Children?.Add(subRoot);
                continue;
            }

            rootNode.Children?.Add(new CategoryTreeNodeDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name,
                Children = null,
                Deletable = subCategory.UserId is not null
            });
        }

        return rootNode;
    }
}
