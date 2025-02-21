using BudgetManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetManagement.Persistence.Configurations;

internal sealed class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("expense_categories", "public");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(300)")
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid")
            .IsRequired(false);

        builder.Property(x => x.RootId)
            .HasColumnName("root_id")
            .HasColumnType("uuid")
            .IsRequired(false);

        builder.HasOne(x => x.Root)
            .WithMany()
            .HasForeignKey(x => x.RootId);
        
        builder.HasData(InitialData.ExpenseCategories);
    }
}
