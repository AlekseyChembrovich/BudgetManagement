using System.Runtime.CompilerServices;

namespace BudgetManagement.Server.Auth;

public sealed class AuthConfiguration
{
    public static string SectionName => nameof(AuthConfiguration);

    public string SecretKey { get; init; } = null!;

    public string Issuer { get; init; } = null!;

    public string Audience { get; init; } = null!;
}
