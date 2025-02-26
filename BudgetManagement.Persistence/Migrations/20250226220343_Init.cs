using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    login = table.Column<string>(type: "varchar(200)", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expense_categories",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar(300)", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    root_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_categories", x => x.id);
                    table.ForeignKey(
                        name: "FK_expense_categories_expense_categories_root_id",
                        column: x => x.root_id,
                        principalSchema: "public",
                        principalTable: "expense_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expense_categories_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "expense_records",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_expense_records_expense_categories_category_id",
                        column: x => x.category_id,
                        principalSchema: "public",
                        principalTable: "expense_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expense_records_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "expense_categories",
                columns: new[] { "id", "name", "root_id", "user_id" },
                values: new object[,]
                {
                    { new Guid("67b3701d-002c-4a72-8a31-09c6bc362c1c"), "Health", null, null },
                    { new Guid("80883d5e-da68-40ac-b7dc-e0c50a63acb7"), "Entertainment", null, null },
                    { new Guid("8b506abc-fa2e-46dd-9cd6-b888e3330a5a"), "Transport", null, null },
                    { new Guid("922e4067-56b5-4a20-987f-43285fd66a43"), "Travel", null, null },
                    { new Guid("974fe237-5533-4898-aa01-912455c656d4"), "Food", null, null },
                    { new Guid("9fa2b238-45b8-4243-b29a-0bf6cd25479c"), "Gifts", null, null },
                    { new Guid("b9faad8d-ee0b-46c7-ac51-505f867c0fd7"), "Savings", null, null },
                    { new Guid("cf137382-a857-4ce9-91ad-7658346ed408"), "Shopping", null, null },
                    { new Guid("f19f6a74-4c97-41bf-9bc7-880dc4f212a5"), "Bills", null, null },
                    { new Guid("fa430763-9dba-444e-b840-46fb5e4f1088"), "Education", null, null }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "users",
                columns: new[] { "id", "login", "password_hash" },
                values: new object[,]
                {
                    { new Guid("0c2ed985-9fa1-415d-8a19-005bd929fd71"), "test@gmail.com", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("20d89285-d9e5-495b-89d5-469d751d111e"), "testtwo@gmail.com", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("515f0613-b25e-41f0-980b-41eef57eae8a"), "testthree@gmail.com", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" },
                    { new Guid("f2c61707-cb8e-44d0-8ed8-3cec4b2d237a"), "testfour@gmail.com", "9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08" }
                });

            migrationBuilder.InsertData(
                schema: "public",
                table: "expense_records",
                columns: new[] { "id", "amount", "category_id", "created_at", "user_id" },
                values: new object[,]
                {
                    { new Guid("1888051e-3b87-4bbf-bce7-0d9ea38dce3c"), 15.30m, new Guid("8b506abc-fa2e-46dd-9cd6-b888e3330a5a"), new DateTime(2025, 2, 26, 22, 3, 43, 13, DateTimeKind.Utc).AddTicks(7934), new Guid("20d89285-d9e5-495b-89d5-469d751d111e") },
                    { new Guid("18d60b25-98c1-4a36-afe6-f0b706b13bff"), 50.75m, new Guid("974fe237-5533-4898-aa01-912455c656d4"), new DateTime(2025, 2, 26, 22, 3, 43, 13, DateTimeKind.Utc).AddTicks(6435), new Guid("0c2ed985-9fa1-415d-8a19-005bd929fd71") },
                    { new Guid("9d8c1f84-6dbf-4476-9039-aec43c5ff873"), 100.00m, new Guid("80883d5e-da68-40ac-b7dc-e0c50a63acb7"), new DateTime(2025, 2, 26, 22, 3, 43, 13, DateTimeKind.Utc).AddTicks(7938), new Guid("515f0613-b25e-41f0-980b-41eef57eae8a") },
                    { new Guid("f4fb757d-92a8-417c-9d34-4303ac3211dc"), 200.50m, new Guid("67b3701d-002c-4a72-8a31-09c6bc362c1c"), new DateTime(2025, 2, 26, 22, 3, 43, 13, DateTimeKind.Utc).AddTicks(7941), new Guid("f2c61707-cb8e-44d0-8ed8-3cec4b2d237a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_expense_categories_root_id",
                schema: "public",
                table: "expense_categories",
                column: "root_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_categories_user_id",
                schema: "public",
                table: "expense_categories",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_records_category_id",
                schema: "public",
                table: "expense_records",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_expense_records_user_id",
                schema: "public",
                table: "expense_records",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expense_records",
                schema: "public");

            migrationBuilder.DropTable(
                name: "expense_categories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
