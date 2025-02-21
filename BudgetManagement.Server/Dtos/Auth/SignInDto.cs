namespace BudgetManagement.Server.Dtos.Auth;

public sealed class SignInDto
{
    public required string Login { get; init; }

    public required string Password { get; init; }
}
