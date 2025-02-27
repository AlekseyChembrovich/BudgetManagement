using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Services;

namespace BudgetManagement.Tests.Services;

public class TreeBuilderTests
{
    private readonly TreeBuilder _treeBuilder = new();

    [Fact]
    public void BuildTree_ReturnsEmptyList_WhenNoCategories()
    {
        // Arrange
        var categories = Array.Empty<ExpenseCategory>();
        
        // Act
        var result = _treeBuilder.BuildTree(categories);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void BuildTree_ReturnsSingleRootNode_WhenOneRootCategory()
    {
        // Arrange
        var rootCategory = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Root", RootId = null };
        var categories = new List<ExpenseCategory> { rootCategory };
        
        // Act
        var result = _treeBuilder.BuildTree(categories);
        
        // Assert
        Assert.Single(result);
        Assert.Equal("Root", result[0].Name);
        Assert.Null(result[0].Children);
    }

    [Fact]
    public void BuildTree_OrganizesCategoriesIntoHierarchy()
    {
        // Arrange
        var rootCategory1 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Root1", RootId = null };
        var rootCategory2 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Root2", RootId = null };
        var rootCategory3 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Root3", RootId = null };
        var childCategory1 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Child1", RootId = rootCategory1.Id };
        var childCategory2 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Child2", RootId = rootCategory1.Id };
        var childCategory3 = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Child3", RootId = rootCategory2.Id };
        var categories = new List<ExpenseCategory> { rootCategory1, rootCategory2, rootCategory3, childCategory1, childCategory2, childCategory3 };
        
        // Act
        var result = _treeBuilder.BuildTree(categories);
        
        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(2, result[0].Children!.Count);
        Assert.Null(result[2].Children);
    }

    [Fact]
    public void BuildTree_SetsDeletableFlagCorrectly()
    {
        // Arrange
        var rootCategory = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Root", RootId = null, UserId = null };
        var childCategory = new ExpenseCategory { Id = Guid.NewGuid(), Name = "Child", RootId = rootCategory.Id, UserId = Guid.NewGuid() };
        var categories = new List<ExpenseCategory> { rootCategory, childCategory };
        
        // Act
        var result = _treeBuilder.BuildTree(categories);
        
        // Assert
        Assert.False(result[0].Deletable);
        Assert.True(result[0].Children![0].Deletable);
    }
}
