﻿// <auto-generated />
using System;
using BudgetManagement.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetManagement.Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BudgetManagement.Core.Entities.ExpenseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(300)")
                        .HasColumnName("name");

                    b.Property<Guid?>("RootId")
                        .HasColumnType("uuid")
                        .HasColumnName("root_id");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("RootId");

                    b.HasIndex("UserId");

                    b.ToTable("expense_categories", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("974fe237-5533-4898-aa01-912455c656d4"),
                            Name = "Food"
                        },
                        new
                        {
                            Id = new Guid("8b506abc-fa2e-46dd-9cd6-b888e3330a5a"),
                            Name = "Transport"
                        },
                        new
                        {
                            Id = new Guid("80883d5e-da68-40ac-b7dc-e0c50a63acb7"),
                            Name = "Entertainment"
                        },
                        new
                        {
                            Id = new Guid("67b3701d-002c-4a72-8a31-09c6bc362c1c"),
                            Name = "Health"
                        },
                        new
                        {
                            Id = new Guid("fa430763-9dba-444e-b840-46fb5e4f1088"),
                            Name = "Education"
                        },
                        new
                        {
                            Id = new Guid("cf137382-a857-4ce9-91ad-7658346ed408"),
                            Name = "Shopping"
                        },
                        new
                        {
                            Id = new Guid("f19f6a74-4c97-41bf-9bc7-880dc4f212a5"),
                            Name = "Bills"
                        },
                        new
                        {
                            Id = new Guid("b9faad8d-ee0b-46c7-ac51-505f867c0fd7"),
                            Name = "Savings"
                        },
                        new
                        {
                            Id = new Guid("9fa2b238-45b8-4243-b29a-0bf6cd25479c"),
                            Name = "Gifts"
                        },
                        new
                        {
                            Id = new Guid("922e4067-56b5-4a20-987f-43285fd66a43"),
                            Name = "Travel"
                        });
                });

            modelBuilder.Entity("BudgetManagement.Core.Entities.ExpenseRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("amount");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid")
                        .HasColumnName("category_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("expense_records", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("18d60b25-98c1-4a36-afe6-f0b706b13bff"),
                            Amount = 50.75m,
                            CategoryId = new Guid("974fe237-5533-4898-aa01-912455c656d4"),
                            CreatedAt = new DateTime(2025, 2, 21, 16, 15, 26, 580, DateTimeKind.Utc).AddTicks(1712),
                            UserId = new Guid("0c2ed985-9fa1-415d-8a19-005bd929fd71")
                        },
                        new
                        {
                            Id = new Guid("1888051e-3b87-4bbf-bce7-0d9ea38dce3c"),
                            Amount = 15.30m,
                            CategoryId = new Guid("8b506abc-fa2e-46dd-9cd6-b888e3330a5a"),
                            CreatedAt = new DateTime(2025, 2, 21, 16, 15, 26, 580, DateTimeKind.Utc).AddTicks(3180),
                            UserId = new Guid("20d89285-d9e5-495b-89d5-469d751d111e")
                        },
                        new
                        {
                            Id = new Guid("9d8c1f84-6dbf-4476-9039-aec43c5ff873"),
                            Amount = 100.00m,
                            CategoryId = new Guid("80883d5e-da68-40ac-b7dc-e0c50a63acb7"),
                            CreatedAt = new DateTime(2025, 2, 21, 16, 15, 26, 580, DateTimeKind.Utc).AddTicks(3184),
                            UserId = new Guid("515f0613-b25e-41f0-980b-41eef57eae8a")
                        },
                        new
                        {
                            Id = new Guid("f4fb757d-92a8-417c-9d34-4303ac3211dc"),
                            Amount = 200.50m,
                            CategoryId = new Guid("67b3701d-002c-4a72-8a31-09c6bc362c1c"),
                            CreatedAt = new DateTime(2025, 2, 21, 16, 15, 26, 580, DateTimeKind.Utc).AddTicks(3186),
                            UserId = new Guid("f2c61707-cb8e-44d0-8ed8-3cec4b2d237a")
                        });
                });

            modelBuilder.Entity("BudgetManagement.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.HasKey("Id");

                    b.ToTable("users", "public");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c2ed985-9fa1-415d-8a19-005bd929fd71"),
                            Login = "test@gmail.com",
                            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        },
                        new
                        {
                            Id = new Guid("20d89285-d9e5-495b-89d5-469d751d111e"),
                            Login = "testtwo@gmail.com",
                            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        },
                        new
                        {
                            Id = new Guid("515f0613-b25e-41f0-980b-41eef57eae8a"),
                            Login = "testthree@gmail.com",
                            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        },
                        new
                        {
                            Id = new Guid("f2c61707-cb8e-44d0-8ed8-3cec4b2d237a"),
                            Login = "testfour@gmail.com",
                            PasswordHash = "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08"
                        });
                });

            modelBuilder.Entity("BudgetManagement.Core.Entities.ExpenseCategory", b =>
                {
                    b.HasOne("BudgetManagement.Core.Entities.ExpenseCategory", "Root")
                        .WithMany()
                        .HasForeignKey("RootId");

                    b.HasOne("BudgetManagement.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Root");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BudgetManagement.Core.Entities.ExpenseRecord", b =>
                {
                    b.HasOne("BudgetManagement.Core.Entities.ExpenseCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetManagement.Core.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
