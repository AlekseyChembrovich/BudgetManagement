using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Tests.Services;

public class ExpenseRecordServiceTests
{
    private readonly DatabaseContext _context;
    private readonly ExpenseRecordService _service;

    public ExpenseRecordServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DatabaseContext(options);
        _service = new ExpenseRecordService(_context);
    }

    [Fact]
    public async Task GetUserRecordsAsync_UserRecord_ReturnsRecords()
    {
        // Arrange
        var category = await SeedCategoryAsync();
        var userId = Guid.NewGuid();
        _ = await SeedRecordAsync(123, category.Id, userId);

        // Act
        var result = await _service.GetUserRecordsAsync(userId, CancellationToken.None);

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, r => r.UserId == userId);
    }
    
    [Fact]
    public async Task GetUserRecordsAsync_UserRecordWithCategory_ReturnsRecords()
    {
        // Arrange
        var rootCategory = await SeedCategoryAsync();
        var subCategory = await SeedCategoryAsync(rootId: rootCategory.Id);
        var userId = Guid.NewGuid();
        _ = await SeedRecordAsync(123, subCategory.Id, userId);

        // Act
        var result = await _service.GetUserRecordsAsync(
            userId,
            rootCategory.Id,
            null,
            null,
            CancellationToken.None);

        // Assert
        Assert.NotEmpty(result);
        Assert.Contains(result, r => r.UserId == userId && r.CategoryId == subCategory.Id);
    }
    
    [Fact]
    public async Task CreateAsync_AddsRecord()
    {
        // Arrange
        const decimal amount = 123;
        var userId = Guid.NewGuid();
        var category = await SeedCategoryAsync();

        // Act
        var result = await _service.CreateAsync(amount, category.Id, userId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(amount, result.Amount);
        var found = await _context.Set<ExpenseRecord>()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.CategoryId == category.Id);
        Assert.NotNull(found);
    }

    [Fact]
    public async Task DeleteAsync_RemovesRecord_WhenExists()
    {
        // Arrange
        var category = await SeedCategoryAsync();
        var record = await SeedRecordAsync(123, category.Id, Guid.NewGuid());

        // Act
        var result = await _service.DeleteAsync(record.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(record.Id, result.Id);
        var found = await _context.Set<ExpenseRecord>()
            .FirstOrDefaultAsync(x => x.Id == record.Id);
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
    public async Task UpdateAsync_UpdatesRecord_WhenExists()
    {
        // Arrange
        var category = await SeedCategoryAsync();
        var record = await SeedRecordAsync(123, category.Id, Guid.NewGuid());
        
        const decimal newAmount = 500;
        var newCategoryId = Guid.NewGuid();

        // Act
        var result = await _service.UpdateAsync(record.Id, newAmount, newCategoryId, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.Amount);
        var found = await _context.Set<ExpenseRecord>()
            .FirstOrDefaultAsync(x => x.Id == record.Id);
        Assert.NotNull(found);
        Assert.Equal(newAmount, found.Amount);
        Assert.Equal(newCategoryId, found.CategoryId);
    }
    
    [Fact]
    public async Task UpdateAsync_ReturnsNull_WhenNotExists()
    {
        // Act
        var result = await _service.UpdateAsync(
            Guid.NewGuid(),
            123,
            Guid.NewGuid(),
            CancellationToken.None);
        
        // Assert
        Assert.Null(result);
    }

    private async Task<ExpenseCategory> SeedCategoryAsync(Guid? userId = null, Guid? rootId = null)
    {
        var category = new ExpenseCategory
        {
            Id = Guid.NewGuid(),
            Name = "Test name",
            UserId = userId,
            RootId = rootId
        };
        var entry = await _context.Set<ExpenseCategory>().AddAsync(category);
        await _context.SaveChangesAsync();
        
        return entry.Entity;
    }
    
    private async Task<ExpenseRecord> SeedRecordAsync(decimal amount, Guid categoryId, Guid userId)
    {
        var record = new ExpenseRecord
        {
            Id = Guid.NewGuid(),
            Amount = amount,
            UserId = userId,
            CategoryId = categoryId,
            CreatedAt = DateTime.UtcNow
        };
        var entry = await _context.Set<ExpenseRecord>().AddAsync(record);
        await _context.SaveChangesAsync();
        
        return entry.Entity;
    }
}
