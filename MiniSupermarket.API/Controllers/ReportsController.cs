
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: /api/reports/revenue-by-cashier
        [HttpGet("revenue-by-cashier")]
        public async Task<IActionResult> GetRevenueByCashier()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Chưa cấu hình DefaultConnection.");

            var reports = new List<CashierRevenueDto>();

            try
            {
                await using var connection =
                    new SqlConnection(connectionString);

                await using var command =
                    new SqlCommand(
                        "dbo.sp_GetRevenueByCashier",
                        connection);

                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                await using var reader =
                    await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    reports.Add(new CashierRevenueDto
                    {
                        CashierUsername =
                            reader["CashierUsername"] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(
                                    reader["CashierUsername"]) ?? string.Empty,

                        TotalOrders =
                            Convert.ToInt32(reader["TotalOrders"]),

                        TotalRevenue =
                            Convert.ToDecimal(reader["TotalRevenue"])
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Lấy báo cáo doanh thu thành công.",
                    totalCashiers = reports.Count,
                    data = reports
                });
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Không thể truy vấn báo cáo doanh thu."
                });
            }
        }
    }

    public class CashierRevenueDto
    {
        public string CashierUsername { get; set; } = string.Empty;

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }
    }
}
