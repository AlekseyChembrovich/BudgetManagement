using BudgetManagement.Core.Helpers;
using BudgetManagement.Persistence;
using BudgetManagement.Server;
using BudgetManagement.Server.Auth;
using Microsoft.OpenApi.Models;

const string corsPolicyName = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer your-token'"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

var authConfiguration = builder.Configuration
    .GetSection(AuthConfiguration.SectionName)
    .Get<AuthConfiguration>();

builder.Services
    .AddDatabase(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddAuthenticationAndAuthorization(authConfiguration!);

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(
        corsPolicyName,
        config =>
            config.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
    );
});

var app = builder.Build();

app.UseCors(corsPolicyName);

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

public partial class Program { }
