using BudgetManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetManagement.Persistence;

public static class ServicesRegistration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // services.AddDbContext<DatabaseContext>(builder => builder.UseInMemoryDatabase("BudgetManagement"));
        services.AddDbContext<DatabaseContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        return services;
    }
}
