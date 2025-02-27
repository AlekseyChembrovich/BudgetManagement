using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Services;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BudgetManagement.Tests.Services;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly Mock<IOptions<AuthConfiguration>> _authConfigMock;

    public AuthServiceTests()
    {
        _authConfigMock = new Mock<IOptions<AuthConfiguration>>();
        _authConfigMock.Setup(x => x.Value).Returns(new AuthConfiguration
        {
            SecretKey = "my_super_123_Truper_Secret_321_pass", // Use a sufficiently long key for HmacSha256
            Issuer = "https://localhost:5004",
            Audience = "https://localhost:5005"
        });

        _authService = new AuthService(_authConfigMock.Object);
    }

    [Fact]
    public async Task GetUserTokenAsync_ValidUser_ReturnsToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Login = "john.doe@example.com"
        };

        // Act
        var token = await _authService.GetUserTokenAsync(user);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(token));
        ValidateJwtToken(token, user.Id, user.Login, PoliciesConfiguration.UserRole);
    }

    private void ValidateJwtToken(string token, Guid expectedId, string expectedEmail, string expectedRole)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_authConfigMock.Object.Value.SecretKey);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = _authConfigMock.Object.Value.Issuer,
            ValidAudience = _authConfigMock.Object.Value.Audience,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var claims = jwtToken.Claims.ToArray();

        Assert.Equal(expectedId.ToString(), claims.FirstOrDefault(x => x.Type == "nameid")?.Value);
        Assert.Equal(expectedEmail, claims.FirstOrDefault(x => x.Type == "email")?.Value);
        Assert.Equal(expectedRole, claims.FirstOrDefault(x => x.Type == "role")?.Value);
    }
}
