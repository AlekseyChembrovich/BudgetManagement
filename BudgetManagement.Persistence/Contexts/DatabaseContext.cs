using BudgetManagement.Core.Entities;
using BudgetManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagement.Persistence.Contexts;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<ExpenseRecord> ExpenseRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new ExpenseCategoryConfiguration());
        builder.ApplyConfiguration(new ExpenseRecordConfiguration());
    }
}
