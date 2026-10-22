
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/ado-products")]
public class AdoProductController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AdoProductController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("barcode/{barcode}")]
    public IActionResult GetProductByBarcode(string barcode)
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
            using var connection =
                new SqlConnection(connectionString);

            using var command =
                new SqlCommand(sql, connection);

            command.Parameters.Add(
                "@Barcode", SqlDbType.NVarChar, 50
            ).Value = barcode.Trim();

            connection.Open();

            using var reader = command.ExecuteReader();

            if (!reader.Read())
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
