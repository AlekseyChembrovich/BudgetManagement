using System.Security.Claims;

namespace BudgetManagement.Server.Extensions;

public static class UserContextExtensions
{
    public static T Id<T>(this ClaimsPrincipal user)
        where T : IParsable<T>
    {
        var userIdAsString = user.FindFirst(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
        
        return T.Parse(userIdAsString!, null);
    }
}
