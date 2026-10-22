using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniSupermarket.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedThanhHoang_MiniSupermarketDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát", "Nước ngọt, nước tăng lực, nước khoáng" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & sản phẩm từ sữa", "Sữa tươi, sữa hộp, sữa chua" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo", "Bánh quy, kẹo, socola" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Đồ ăn vặt", "Snack, rong biển, đồ ăn nhẹ" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì & thực phẩm ăn liền", "Mì gói, phở, cháo ăn liền" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 6, "Gạo & thực phẩm khô", "Gạo, đậu, các loại hạt" },
                    { 7, "Gia vị & dầu ăn", "Nước mắm, nước tương, dầu ăn" },
                    { 8, "Thực phẩm đóng hộp", "Cá hộp, thịt hộp, pate" },
                    { 9, "Đồ uống dinh dưỡng", "Sữa hạt, nước yến, thức uống dinh dưỡng" },
                    { 10, "Cà phê & trà", "Cà phê hòa tan, cà phê gói, trà" },
                    { 11, "Hóa mỹ phẩm", "Dầu gội, sữa tắm, sản phẩm chăm sóc cơ thể" },
                    { 12, "Chăm sóc cá nhân", "Kem đánh răng, khăn giấy, khẩu trang" },
                    { 13, "Đồ dùng gia đình", "Nước rửa chén, túi rác, đồ dùng gia đình" },
                    { 14, "Văn phòng phẩm", "Bút, vở, dụng cụ học tập" },
                    { 15, "Đồ dùng tiện ích", "Pin, bật lửa và các vật dụng tiện ích" }
                });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { "Quận 10, TP.HCM", "Nguyễn Văn An", 850 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { "Quận 3, TP.HCM", "Trần Thị Bình", 520 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { "Quận 5, TP.HCM", "Lê Văn Cường", 120 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Address", "CustomerName", "MembershipRank", "PhoneNumber", "RewardPoints" },
                values: new object[,]
                {
                    { 4, "Quận 1, TP.HCM", "Phạm Thị Dung", "Vàng", "0934567812", 1250 },
                    { 5, "Quận 11, TP.HCM", "Hoàng Minh Đức", "Bạc", "0978123456", 320 },
                    { 6, "Tân Bình, TP.HCM", "Võ Thị Hạnh", "Chuẩn", "0908765432", 90 },
                    { 7, "Bình Thạnh, TP.HCM", "Đặng Quốc Huy", "Bạc", "0912345678", 680 },
                    { 8, "Gò Vấp, TP.HCM", "Bùi Ngọc Lan", "Vàng", "0987654321", 1580 },
                    { 9, "Phú Nhuận, TP.HCM", "Nguyễn Thị Mai", "Chuẩn", "0945678123", 210 },
                    { 10, "Tân Phú, TP.HCM", "Trương Minh Nam", "Bạc", "0961234789", 760 },
                    { 11, "Quận 6, TP.HCM", "Phan Thị Oanh", "Vàng", "0903456789", 1800 },
                    { 12, "Quận 7, TP.HCM", "Đỗ Hoàng Phúc", "Chuẩn", "0973456128", 60 },
                    { 13, "Thủ Đức, TP.HCM", "Lý Minh Quân", "Bạc", "0937894561", 430 },
                    { 14, "Bình Tân, TP.HCM", "Nguyễn Ngọc Thảo", "Vàng", "0915678234", 1020 },
                    { 15, "Quận 12, TP.HCM", "Trần Minh Tú", "Chuẩn", "0981234567", 150 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Barcode", "CategoryId", "Price", "ProductName", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "8938505970011", 1, 6000m, "Nước suối Aquafina 500ml", 120 },
                    { 2, "8935049500028", 1, 10000m, "Coca Cola lon 330ml", 100 },
                    { 3, "8934673700035", 2, 7500m, "Sữa tươi Vinamilk 180ml", 80 },
                    { 4, "8934680030042", 3, 22000m, "Bánh Oreo vị chocolate 133g", 50 },
                    { 5, "8934572900059", 4, 18000m, "Snack khoai tây Lay's 95g", 70 },
                    { 6, "8934563150066", 5, 4500m, "Mì Hảo Hảo tôm chua cay", 150 },
                    { 7, "8938505970073", 6, 145000m, "Gạo ST25 túi 5kg", 30 },
                    { 8, "8934588010080", 7, 32000m, "Nước mắm Nam Ngư 500ml", 45 },
                    { 9, "8934564700097", 8, 28000m, "Cá ngừ đóng hộp 170g", 35 },
                    { 10, "8934673200103", 10, 42000m, "Cà phê hòa tan G7 3in1", 60 },
                    { 11, "8935217100110", 11, 89000m, "Dầu gội Sunsilk 650g", 25 },
                    { 12, "8935001700127", 12, 38000m, "Kem đánh răng P/S 180g", 40 },
                    { 13, "8934868000134", 13, 35000m, "Nước rửa chén Sunlight 750ml", 55 },
                    { 14, "8938501230141", 14, 18000m, "Vở học sinh 200 trang", 40 },
                    { 15, "8936001250158", 15, 45000m, "Pin AA Panasonic 4 viên", 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 15);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Bánh kẹo & Đồ ăn vặt", "Snack, bánh quy, kẹo dẻo" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Nước giải khát & Trà", "Nước ngọt, nước khoáng, trà" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Sữa & Sản phẩm từ sữa", "Sữa tươi, sữa chua, phô mai" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Mì gói & Thực phẩm ăn liền", "Mì ăn liền, phở khô, cháo gói" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 5,
                columns: new[] { "CategoryName", "Description" },
                values: new object[] { "Gia vị & Dầu ăn", "Nước mắm, hạt nêm, dầu thực vật" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { null, "Nguyễn Văn A", 150 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { null, "Trần Thị B", 50 });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 3,
                columns: new[] { "Address", "CustomerName", "RewardPoints" },
                values: new object[] { null, "Lê Văn C", 10 });
        }
    }
}
