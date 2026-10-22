using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerAddresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "15 Đường Thành Thái, Phường 12, Quận 10, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "28 Đường Võ Văn Tần, Phường 6, Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "42 Đường Nguyễn Trãi, Phường 3, Quận 5, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "56 Đường Nguyễn Huệ, Phường Bến Nghé, Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "73 Đường Lãnh Binh Thăng, Phường 12, Quận 11, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "91 Đường Cộng Hòa, Phường 4, Quận Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "36 Đường Xô Viết Nghệ Tĩnh, Phường 21, Quận Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "108 Đường Quang Trung, Phường 10, Quận Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "64 Đường Phan Đình Phùng, Phường 2, Quận Phú Nhuận, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "125 Đường Lê Trọng Tấn, Phường Sơn Kỳ, Quận Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "39 Đường Hậu Giang, Phường 5, Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "82 Đường Nguyễn Thị Thập, Phường Tân Quy, Quận 7, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "47 Đường Võ Văn Ngân, Phường Linh Chiểu, TP. Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "116 Đường Tên Lửa, Phường Bình Trị Đông B, Quận Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "53 Đường Nguyễn Văn Quá, Phường Đông Hưng Thuận, Quận 12, TP.HCM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                column: "Address",
                value: "Quận 10, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                column: "Address",
                value: "Quận 3, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                column: "Address",
                value: "Quận 5, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4,
                column: "Address",
                value: "Quận 1, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5,
                column: "Address",
                value: "Quận 11, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6,
                column: "Address",
                value: "Tân Bình, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7,
                column: "Address",
                value: "Bình Thạnh, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8,
                column: "Address",
                value: "Gò Vấp, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9,
                column: "Address",
                value: "Phú Nhuận, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10,
                column: "Address",
                value: "Tân Phú, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11,
                column: "Address",
                value: "Quận 6, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12,
                column: "Address",
                value: "Quận 7, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13,
                column: "Address",
                value: "Thủ Đức, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14,
                column: "Address",
                value: "Bình Tân, TP.HCM");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15,
                column: "Address",
                value: "Quận 12, TP.HCM");
        }
    }
}
