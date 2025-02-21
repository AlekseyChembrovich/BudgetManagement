## Применение миграций

В командной строке последовательно выполнить следующие комманды:

1. `dotnet ef migrations add Init --project BudgetManagement.Persistence\BudgetManagement.Persistence.csproj --startup-project BudgetManagement.Server\BudgetManagement.Server.csproj --context BudgetManagement.Persistence.Contexts.DatabaseContext --output-dir Migrations`
2. `dotnet ef database update --project BudgetManagement.Persistence\BudgetManagement.Persistence.csproj --startup-project BudgetManagement.Server\BudgetManagement.Server.csproj --context BudgetManagement.Persistence.Contexts.DatabaseContext --connection "Server=localhost;Port=5006;User ID=qwerty;Database=budget;Password=postgres;TrustServerCertificate=True"`
