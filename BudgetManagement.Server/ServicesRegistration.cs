using System.Text;
using BudgetManagement.Server.Auth;
using BudgetManagement.Server.Services;
using BudgetManagement.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BudgetManagement.Server;

internal static class ServicesRegistration
{
    internal static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
        services.AddScoped<IExpenseRecordService, ExpenseRecordService>();
        services.AddScoped<ITreeBuilder, TreeBuilder>();

        services.Configure<AuthConfiguration>(configuration.GetSection(AuthConfiguration.SectionName));

        services.AddAutoMapper(typeof(Program).Assembly);

        return services;
    }

    internal static IServiceCollection AddAuthenticationAndAuthorization(
        this IServiceCollection services,
        AuthConfiguration authConfiguration)
    {
        services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = authConfiguration.Issuer,
                    ValidAudience = authConfiguration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.SecretKey))
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                PoliciesConfiguration.UserPolicy,
                policy => policy.RequireRole(PoliciesConfiguration.UserRole)
            );
        });
        
        return services;
    }
}
