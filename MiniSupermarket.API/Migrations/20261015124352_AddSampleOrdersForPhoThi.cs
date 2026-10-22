using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleOrdersForPhoThi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CashierUsername", "CreatedDate", "CustomerId", "FinalAmount", "OrderCode", "RewardPoints", "TotalItems" },
                values: new object[,]
                {
                    { 1, "cashier01", new DateTime(2026, 10, 10, 8, 15, 0, 0, DateTimeKind.Unspecified), 1, 54500m, "PT-SEED-20261015-001", 2, 9 },
                    { 2, "cashier02", new DateTime(2026, 10, 10, 9, 20, 0, 0, DateTimeKind.Unspecified), 2, 52000m, "PT-SEED-20261015-002", 1, 5 },
                    { 3, "cashier03", new DateTime(2026, 10, 10, 10, 35, 0, 0, DateTimeKind.Unspecified), 3, 81000m, "PT-SEED-20261015-003", 1, 12 },
                    { 4, "cashier01", new DateTime(2026, 10, 11, 8, 45, 0, 0, DateTimeKind.Unspecified), 4, 209000m, "PT-SEED-20261015-004", 3, 3 },
                    { 5, "cashier04", new DateTime(2026, 10, 11, 11, 10, 0, 0, DateTimeKind.Unspecified), 5, 96000m, "PT-SEED-20261015-005", 2, 6 },
                    { 6, "cashier05", new DateTime(2026, 10, 11, 14, 30, 0, 0, DateTimeKind.Unspecified), 6, 60000m, "PT-SEED-20261015-006", 1, 4 },
                    { 7, "cashier02", new DateTime(2026, 10, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, 102500m, "PT-SEED-20261015-007", 2, 9 },
                    { 8, "cashier06", new DateTime(2026, 10, 12, 10, 50, 0, 0, DateTimeKind.Unspecified), 8, 175000m, "PT-SEED-20261015-008", 3, 5 },
                    { 9, "cashier03", new DateTime(2026, 10, 12, 15, 25, 0, 0, DateTimeKind.Unspecified), 9, 92000m, "PT-SEED-20261015-009", 1, 3 },
                    { 10, "cashier04", new DateTime(2026, 10, 13, 8, 20, 0, 0, DateTimeKind.Unspecified), 10, 173000m, "PT-SEED-20261015-010", 2, 3 },
                    { 11, "cashier01", new DateTime(2026, 10, 13, 10, 15, 0, 0, DateTimeKind.Unspecified), 11, 146000m, "PT-SEED-20261015-011", 3, 4 },
                    { 12, "cashier05", new DateTime(2026, 10, 13, 13, 40, 0, 0, DateTimeKind.Unspecified), 12, 99000m, "PT-SEED-20261015-012", 1, 13 },
                    { 13, "cashier06", new DateTime(2026, 10, 14, 9, 30, 0, 0, DateTimeKind.Unspecified), 13, 120000m, "PT-SEED-20261015-013", 2, 7 },
                    { 14, "cashier02", new DateTime(2026, 10, 14, 14, 10, 0, 0, DateTimeKind.Unspecified), 14, 88000m, "PT-SEED-20261015-014", 3, 6 },
                    { 15, "cashier03", new DateTime(2026, 10, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 15, 87000m, "PT-SEED-20261015-015", 1, 7 }
                });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderDetailId", "LineTotal", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 12000m, 1, 1, 2, 6000m },
                    { 2, 20000m, 1, 2, 2, 10000m },
                    { 3, 22500m, 1, 6, 5, 4500m },
                    { 4, 30000m, 2, 3, 4, 7500m },
                    { 5, 22000m, 2, 4, 1, 22000m },
                    { 6, 36000m, 3, 5, 2, 18000m },
                    { 7, 45000m, 3, 6, 10, 4500m },
                    { 8, 145000m, 4, 7, 1, 145000m },
                    { 9, 64000m, 4, 8, 2, 32000m },
                    { 10, 40000m, 5, 2, 4, 10000m },
                    { 11, 56000m, 5, 9, 2, 28000m },
                    { 12, 18000m, 6, 1, 3, 6000m },
                    { 13, 42000m, 6, 10, 1, 42000m },
                    { 14, 44000m, 7, 4, 2, 22000m },
                    { 15, 36000m, 7, 5, 2, 18000m },
                    { 16, 22500m, 7, 6, 5, 4500m },
                    { 17, 145000m, 8, 7, 1, 145000m },
                    { 18, 30000m, 8, 3, 4, 7500m },
                    { 19, 64000m, 9, 8, 2, 32000m },
                    { 20, 28000m, 9, 9, 1, 28000m },
                    { 21, 84000m, 10, 10, 2, 42000m },
                    { 22, 89000m, 10, 11, 1, 89000m },
                    { 23, 76000m, 11, 12, 2, 38000m },
                    { 24, 70000m, 11, 13, 2, 35000m },
                    { 25, 54000m, 12, 14, 3, 18000m },
                    { 26, 45000m, 12, 6, 10, 4500m },
                    { 27, 90000m, 13, 15, 2, 45000m },
                    { 28, 30000m, 13, 1, 5, 6000m },
                    { 29, 30000m, 14, 2, 3, 10000m },
                    { 30, 22000m, 14, 4, 1, 22000m },
                    { 31, 36000m, 14, 5, 2, 18000m },
                    { 32, 45000m, 15, 3, 6, 7500m },
                    { 33, 42000m, 15, 10, 1, 42000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "OrderDetails",
                keyColumn: "OrderDetailId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15);
        }
    }
}
