using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Tests.Services;

public class ExpenseCategoryServiceTests
{
    private readonly DatabaseContext _context;
    private readonly ExpenseCategoryService _service;

    public ExpenseCategoryServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DatabaseContext(options);
        _service = new ExpenseCategoryService(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCategory_WhenExists()
    {
        // Arrange
        var category = await SeedCategoryAsync("Food");

        // Act
        var result = await _service.GetByIdAsync(category.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var result = await _service.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetUserCategoriesAsync_ReturnsCategories()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _ = await SeedCategoryAsync("Food", userId);

        // Act
        var result = await _service.GetUserCategoriesAsync(userId, CancellationToken.None);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, c => Assert.True(c.UserId == userId || c.UserId == null));
    }

    [Fact]
    public async Task CreateAsync_AddsCategory()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string categoryName = "Entertainment";
        
        // Act
        var result = await _service.CreateAsync(categoryName, userId, null, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryName, result.Name);
        var found = await _context.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.Id == result.Id);
        Assert.NotNull(found);
    }

    [Fact]
    public async Task DeleteAsync_RemovesCategory_WhenExists()
    {
        // Arrange
        var category = await SeedCategoryAsync("Food");

        // Act
        var result = await _service.DeleteAsync(category.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Id, result.Id);
        var found = await _context.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.Id == category.Id);
        Assert.Null(found);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var result = await _service.DeleteAsync(Guid.NewGuid(), CancellationToken.None);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesCategory_WhenExists()
    {
        // Arrange
        var category = await SeedCategoryAsync("Old Name");
        
        const string newName = "Updated Name";

        // Act
        var result = await _service.UpdateAsync(category.Id, newName, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newName, result.Name);
        var found = await _context.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.Id == category.Id);
        Assert.NotNull(found);
        Assert.Equal(newName, found.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var result = await _service.UpdateAsync(Guid.NewGuid(), "New Name", CancellationToken.None);
        
        // Assert
        Assert.Null(result);
    }
    
    private async Task<ExpenseCategory> SeedCategoryAsync(string name, Guid? userId = null, Guid? rootId = null)
    {
        var category = new ExpenseCategory
        {
            Id = Guid.NewGuid(),
            Name = name,
            UserId = userId,
            RootId = rootId
        };
        var entry = await _context.Set<ExpenseCategory>().AddAsync(category);
        await _context.SaveChangesAsync();
        
        return entry.Entity;
    }
}