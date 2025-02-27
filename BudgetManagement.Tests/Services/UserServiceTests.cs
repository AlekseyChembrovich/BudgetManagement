using BudgetManagement.Core.Entities;
using BudgetManagement.Core.Helpers;
using BudgetManagement.Persistence.Contexts;
using BudgetManagement.Server.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Tests.Services;

public class UserServiceTests
{
    private readonly DatabaseContext _context;
    private readonly UserService _service;

    public UserServiceTests()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new DatabaseContext(options);
        _service = new UserService(_context);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser_WhenExists()
    {
        // Arrange
        var user = await SeedUserAsync("test@gmail.com", "test");

        // Act
        var result = await _service.GetByIdAsync(user.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
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
    public async Task GetByLoginAsync_ReturnsUser_WhenExists()
    {
        // Arrange
        const string login = "test@gmail.com";
        var user = await SeedUserAsync(login, "test");

        // Act
        var result = await _service.GetByLoginAsync(login, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(login, result.Login);
    }

    [Fact]
    public async Task GetByLoginAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        const string invalidLogin = "non_existent_user@gmail.com";
        
        // Act
        var result = await _service.GetByLoginAsync(invalidLogin, CancellationToken.None);
        
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_AddsUser()
    {
        // Arrange
        const string login = "test@gmail.com";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = login,
            PasswordHash = "test".GetPasswordHash()
        };
        
        // Act
        var result = await _service.CreateAsync(user, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(login, result.Login);
        var found = await _context.Set<User>()
            .FirstOrDefaultAsync(x => x.Id == result.Id);
        Assert.NotNull(found);
    }
    
    private async Task<User> SeedUserAsync(string login, string password)
    {
        var user = new User { Id = Guid.NewGuid(), Login = login, PasswordHash = password.GetPasswordHash() };
        var entry = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        return entry.Entity;
    }
}
