using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Challenge.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    AvailableBalance = table.Column<long>(type: "bigint", nullable: true),
                    BlockedBalance = table.Column<long>(type: "bigint", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Stock = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Balances",
                columns: new[] { "Id", "AvailableBalance", "BlockedBalance", "Currency", "LastUpdated", "UserId" },
                values: new object[] { new Guid("00000000-aaaa-bbbb-cccc-111111111111"), 10000000000L, 0L, "USD", new DateTime(2023, 6, 15, 10, 30, 0, 0, DateTimeKind.Utc), new Guid("550e8400-e29b-41d4-a716-446655440000") });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Currency", "Description", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4111-aaaa-111111111111"), "Electronics", "USD", "Latest model with advanced features", "Premium Smartphone", 19.989999999999998, 42 },
                    { new Guid("b2c3d4e5-f6a7-4222-bbbb-222222222222"), "Electronics", "USD", "Noise-cancelling with premium sound quality", "Wireless Headphones", 14.99, 78 },
                    { new Guid("c3d4e5f6-a7b8-4333-cccc-333333333333"), "Electronics", "USD", "Fitness tracking and notifications", "Smart Watch", 12.99, 0 },
                    { new Guid("d4e5f6a7-b8c9-4444-dddd-444444444444"), "Electronics", "USD", "High-performance for work and gaming", "Laptop", 19.989999999999998, 15 },
                    { new Guid("e5f6a7b8-c9d0-4555-eeee-555555555555"), "Accessories", "USD", "Fast charging for compatible devices", "Wireless Charger", 9.9900000000000002, 120 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "PreOrders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
