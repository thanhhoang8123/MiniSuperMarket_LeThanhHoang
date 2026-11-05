using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using Microsoft.Data.SqlClient;

using System.Data;



namespace MiniSupermarket.API.Controllers;



[ApiController]

[Route("api/ado-products")]

[Authorize(Roles = "Admin,Cashier")]

public class AdoProductController : ControllerBase

{

    private readonly IConfiguration _configuration;



    public AdoProductController(IConfiguration configuration)

    {

        _configuration = configuration;

    }



    [HttpGet("barcode/{barcode}")]

    public async Task<IActionResult> GetProductByBarcode(string barcode)

    {

        if (string.IsNullOrWhiteSpace(barcode))

        {

            return BadRequest(new

            {

                message = "Mã vạch không được để trống."

            });

        }



        string connectionString =

            _configuration.GetConnectionString("DefaultConnection")

            ?? throw new InvalidOperationException(

                "Chưa cấu hình DefaultConnection.");



        const string sql = @"

            SELECT ProductId, Barcode, ProductName,

                   Price, StockQuantity

            FROM Products

            WHERE Barcode = @Barcode";



        try

        {

            // 1. KHỞI TẠO KẾT NỐI VÀ LỆNH ADO.NET (SqlConnection & SqlCommand)

            // Bọc trong 'await using' để tự động giải phóng tài nguyên kết nối an toàn

            await using var connection =
                new SqlConnection(connectionString);
            await using var command =

                new SqlCommand(sql, connection);
            // 2. TRUYỀN THAM SỐ QUA PARAMETER CHỐNG TẤN CÔNG SQL INJECTION (Tuyệt đối không cộng chuỗi)
            command.Parameters.Add(
                "@Barcode", SqlDbType.NVarChar, 50
            ).Value = barcode.Trim();
            // Mở kết nối bất đồng bộ đến cơ sở dữ liệu
            await connection.OpenAsync();
            // 3. THỰC THI TRUY VẤN VÀ ĐỌC DỮ LIỆU BẰNG SqlDataReader (Đọc 1 chiều siêu tốc)

            await using var reader = await command.ExecuteReaderAsync();
            if (!await reader.ReadAsync())

            {
                return NotFound(new

                {
                    message = "Không tìm thấy sản phẩm."
                });
            }
            var product = new
            {
                ProductId = reader.GetInt32(

                    reader.GetOrdinal("ProductId")),
                Barcode = reader.GetString(

                    reader.GetOrdinal("Barcode")),
                ProductName = reader.GetString(
                    reader.GetOrdinal("ProductName")),
                Price = reader.GetDecimal(
                    reader.GetOrdinal("Price")),
                StockQuantity = reader.GetInt32(

                    reader.GetOrdinal("StockQuantity"))

            };
            return Ok(product);

        }

        catch (SqlException)

        {

            return StatusCode(500, new

            {

                message = "Lỗi truy vấn cơ sở dữ liệu."

            });

        }

    }

}

