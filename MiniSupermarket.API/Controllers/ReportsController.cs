using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using System.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;
        private readonly ILogger<ReportsController> _logger;

        public ReportsController(IConfiguration configuration, SupermarketDbContext context, ILogger<ReportsController> logger)
        {
            _configuration = configuration;
            _context = context;
            _logger = logger;
        }

        // =========================================================
        // THỐNG KÊ TỔNG QUAN DASHBOARD (KPI SUMMARY CARDS)
        // =========================================================
        // GET: /api/reports/dashboard-summary
        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);

                // 1. Doanh thu hôm nay (VNĐ) - Tổng tiền các hóa đơn phát sinh trong ngày
                var todayRevenue = await _context.Orders
                    .Where(o => o.CreatedDate >= today && o.CreatedDate < tomorrow)
                    .SumAsync(o => (decimal?)o.FinalAmount) ?? 0m;

                // 2. Tổng số hóa đơn đã xuất trên toàn hệ thống
                var totalOrders = await _context.Orders.CountAsync();

                // 3. Số lượng khách hàng thành viên đã đăng ký
                var newCustomers = await _context.Customers
                    .CountAsync(c => c.RegisteredAt >= today && c.RegisteredAt < tomorrow);

                // 4. Số lượng mặt hàng sắp hết kho (tồn kho <= 10) cần nhập thêm
                var lowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 10);

                var summary = new DashboardSummaryDto
                {
                    TodayRevenue = todayRevenue,
                    TotalOrders = totalOrders,
                    NewCustomers = newCustomers,
                    LowStockProducts = lowStockProducts
                };

                return Ok(new
                {
                    success = true,
                    message = "Lấy dữ liệu tổng quan dashboard thành công.",
                    data = summary
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Không thể tải các chỉ số KPI của Dashboard.");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Không thể tải dữ liệu KPI. Hãy kiểm tra kết nối cơ sở dữ liệu và các migration đã được áp dụng."
                });
            }
        }

        // =========================================================
        // BÁO CÁO DOANH THU THEO KHOẢNG THỜI GIAN (BIỂU ĐỒ CỘT)
        // Gom nhóm GROUP BY CAST(CreatedDate AS DATE) qua Stored Procedure
        // =========================================================
        // GET: /api/reports/daily-revenue?fromDate=yyyy-MM-dd&toDate=yyyy-MM-dd
        [HttpGet("daily-revenue")]
        public async Task<IActionResult> GetDailyRevenue([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var from = fromDate ?? DateTime.Today.AddDays(-7);
            var to = toDate ?? DateTime.Today;

            var reports = new List<DailyRevenueDto>();
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Chưa cấu hình DefaultConnection.");

            try
            {
                // Gọi Stored Procedure dbo.sp_GetDailyRevenue bằng ADO.NET
                await using var connection = new SqlConnection(connectionString);
                await using var command = new SqlCommand("dbo.sp_GetDailyRevenue", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FromDate", from.Date);
                command.Parameters.AddWithValue("@ToDate", to.Date);

                await connection.OpenAsync();
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var dateVal = reader["Date"];
                    var dateStr = dateVal != DBNull.Value
                        ? Convert.ToDateTime(dateVal).ToString("dd/MM/yyyy")
                        : string.Empty;

                    reports.Add(new DailyRevenueDto
                    {
                        Date = dateStr,
                        TotalOrders = Convert.ToInt32(reader["TotalOrders"]),
                        TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                    });
                }
            }
            catch (Exception)
            {
                // Dự phòng an toàn: Lấy danh sách hóa đơn theo khoảng thời gian và gom nhóm bộ nhớ
                var nextDay = to.Date.AddDays(1);
                var ordersList = await _context.Orders
                    .AsNoTracking()
                    .Where(o => o.CreatedDate >= from.Date && o.CreatedDate < nextDay)
                    .Select(o => new { o.OrderId, o.CreatedDate, o.FinalAmount })
                    .ToListAsync();

                reports = ordersList
                    .GroupBy(o => o.CreatedDate.Date)
                    .OrderBy(g => g.Key)
                    .Select(g => new DailyRevenueDto
                    {
                        Date = g.Key.ToString("dd/MM/yyyy"),
                        TotalOrders = g.Count(),
                        TotalRevenue = g.Sum(x => x.FinalAmount)
                    })
                    .ToList();
            }

            return Ok(new
            {
                success = true,
                message = "Lấy dữ liệu doanh thu theo ngày thành công.",
                totalDays = reports.Count,
                data = reports
            });
        }

        // =========================================================
        // BÁO CÁO DOANH THU THEO NHÂN VIÊN QUA STORED PROCEDURE
        // =========================================================
        // GET: /api/reports/revenue-by-cashier
        [HttpGet("revenue-by-cashier")]
        public async Task<IActionResult> GetRevenueByCashier()
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Chưa cấu hình DefaultConnection.");

            var reports = new List<CashierRevenueDto>();

            try
            {
                // Khởi tạo kết nối và gọi Stored Procedure bằng SqlCommand
                // Bọc trong 'await using' tự động giải phóng tài nguyên an toàn
                await using var connection = new SqlConnection(connectionString);

                await using var command = new SqlCommand("dbo.sp_GetRevenueByCashier", connection);
                // Thiết lập CommandType là StoredProcedure
                command.CommandType = CommandType.StoredProcedure;

                await connection.OpenAsync();

                // Đọc dữ liệu báo cáo bằng SqlDataReader duyệt một chiều siêu tốc
                await using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    reports.Add(new CashierRevenueDto
                    {
                        CashierUsername =
                            reader["CashierUsername"] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(reader["CashierUsername"]) ?? string.Empty,

                        TotalOrders = Convert.ToInt32(reader["TotalOrders"]),

                        TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
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
            catch (Exception)
            {
                // Dự phòng an toàn: LINQ EF Core gom nhóm theo CashierUsername
                reports = await _context.Orders
                    .AsNoTracking()
                    .Where(o => !string.IsNullOrEmpty(o.CashierUsername))
                    .GroupBy(o => o.CashierUsername)
                    .Select(g => new CashierRevenueDto
                    {
                        CashierUsername = g.Key,
                        TotalOrders = g.Count(),
                        TotalRevenue = g.Sum(x => x.FinalAmount)
                    })
                    .OrderByDescending(x => x.TotalRevenue)
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lấy báo cáo doanh thu thành công.",
                    totalCashiers = reports.Count,
                    data = reports
                });
            }
        }

        // =========================================================
        // THỐNG KÊ TOP 5 SẢN PHẨM BÁN CHẠY NHẤT (BEST-SELLERS)
        // LINQ: GroupBy(od => od.ProductId) -> OrderByDescending(Sum Quantity) -> Take(5)
        // =========================================================
        // GET: /api/reports/top-selling-products?limit=5
        [HttpGet("top-selling-products")]
        public async Task<IActionResult> GetTopSellingProducts([FromQuery] int limit = 5)
        {
            try
            {
                int topCount = limit > 0 ? limit : 5;

                // Thống kê số lượng bán và doanh thu theo ProductId bằng LINQ
                var topProducts = await _context.OrderDetails
                    .GroupBy(od => od.ProductId)
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        TotalQuantitySold = g.Sum(od => od.Quantity),
                        TotalRevenue = g.Sum(od => od.LineTotal)
                    })
                    .OrderByDescending(x => x.TotalQuantitySold)
                    .Take(topCount)
                    .Join(_context.Products,
                        stat => stat.ProductId,
                        prod => prod.ProductId,
                        (stat, prod) => new TopProductDto
                        {
                            ProductId = stat.ProductId,
                            Barcode = prod.Barcode,
                            ProductName = prod.ProductName,
                            TotalQuantitySold = stat.TotalQuantitySold,
                            TotalRevenue = stat.TotalRevenue
                        })
                    .OrderByDescending(x => x.TotalQuantitySold)
                    .ToListAsync();

                return Ok(new
                {
                    success = true,
                    message = "Lấy danh sách sản phẩm bán chạy thành công.",
                    total = topProducts.Count,
                    data = topProducts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Không thể thống kê sản phẩm bán chạy: " + ex.Message
                });
            }
        }
    }
}

