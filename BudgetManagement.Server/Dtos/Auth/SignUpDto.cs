namespace BudgetManagement.Server.Dtos.Auth;

public sealed class SignUpDto
{
    public required string Login { get; init; }

    public required string Password { get; init; }

    public required string ConfirmPassword { get; init; }
}
