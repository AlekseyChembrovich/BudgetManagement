using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BudgetManagement.Core.Entities;
using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BudgetManagement.Server.Services;

internal sealed class AuthService(IOptions<AuthConfiguration> authConfiguration) : IAuthService
{
    private readonly AuthConfiguration _authConfiguration = authConfiguration.Value;

    public Task<string> GetUserTokenAsync(User user)
    {
        var jwtToken = GetToken(user.Id, user.Login);
        
        return Task.FromResult(jwtToken);
    }

    private string GetToken(Guid id, string login)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, id.ToString()),
            new (ClaimTypes.Email, login),
            new (ClaimTypes.Role, PoliciesConfiguration.UserRole)
        };
        
        var claimsIdentity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var jwtToken = GenerateJwtToken(claimsPrincipal);
        
        return jwtToken;
    }
    
    private string GenerateJwtToken(ClaimsPrincipal claimsPrincipal)
    {
        var keyBytes = Encoding.UTF8.GetBytes(_authConfiguration.SecretKey);
        var key = new SymmetricSecurityKey(keyBytes);
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimsPrincipal.Claims),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _authConfiguration.Issuer,
            Audience = _authConfiguration.Audience,
            SigningCredentials = credentials
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}
