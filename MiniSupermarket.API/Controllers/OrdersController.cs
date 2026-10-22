using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        private readonly IConfiguration _configuration;

        public OrdersController(
            SupermarketDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // =====================================================
        // POST: /api/orders/checkout
        // =====================================================

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
      [FromBody] CheckoutRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Giỏ hàng đang trống!"
                });
            }

            // Lấy username từ JWT đã được xác thực
            string cashierUsername =
                User.Identity?.Name ?? string.Empty;

            if (string.IsNullOrWhiteSpace(cashierUsername))
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Không xác định được tài khoản đang đăng nhập."
                });
            }

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "Chưa cấu hình DefaultConnection.");

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction =
                (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                // 1. TÌM KHÁCH HÀNG
                int customerId;

                if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
                {
                    const string sqlCustomer = @"
                SELECT CustomerId
                FROM Customers
                WHERE PhoneNumber = @PhoneNumber;";

                    await using var cmdCustomer =
                        new SqlCommand(sqlCustomer, connection, transaction);

                    cmdCustomer.Parameters.Add(
                        "@PhoneNumber", SqlDbType.VarChar, 15).Value =
                        request.CustomerPhone.Trim();

                    object? result = await cmdCustomer.ExecuteScalarAsync();

                    if (result == null || result == DBNull.Value)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            success = false,
                            message = "Không tìm thấy khách hàng với số điện thoại này!"
                        });
                    }

                    customerId = Convert.ToInt32(result);
                }
                else
                {
                    // Tìm khách vãng lai
                    const string sqlGuest = @"
                SELECT CustomerId
                FROM Customers
                WHERE PhoneNumber = @PhoneNumber;";

                    await using var cmdGuest =
                        new SqlCommand(sqlGuest, connection, transaction);

                    cmdGuest.Parameters.Add(
                        "@PhoneNumber", SqlDbType.VarChar, 15).Value =
                        "0000000000";

                    object? guestResult = await cmdGuest.ExecuteScalarAsync();

                    if (guestResult != null && guestResult != DBNull.Value)
                    {
                        customerId = Convert.ToInt32(guestResult);
                    }
                    else
                    {
                        const string sqlInsertGuest = @"
                    INSERT INTO Customers
                        (CustomerName, PhoneNumber, Address,
                         RewardPoints, MembershipRank)
                    OUTPUT INSERTED.CustomerId
                    VALUES
                        (@CustomerName, @PhoneNumber, @Address,
                         0, @MembershipRank);";

                        await using var cmdInsertGuest =
                            new SqlCommand(
                                sqlInsertGuest, connection, transaction);

                        cmdInsertGuest.Parameters.Add(
                            "@CustomerName", SqlDbType.NVarChar, 100).Value =
                            "Khách vãng lai";

                        cmdInsertGuest.Parameters.Add(
                            "@PhoneNumber", SqlDbType.VarChar, 15).Value =
                            "0000000000";

                        cmdInsertGuest.Parameters.Add(
                            "@Address", SqlDbType.NVarChar, 200).Value =
                            "";

                        cmdInsertGuest.Parameters.Add(
                            "@MembershipRank", SqlDbType.NVarChar, 50).Value =
                            "Chuẩn";

                        customerId = Convert.ToInt32(
                            await cmdInsertGuest.ExecuteScalarAsync());
                    }
                }

                // 2. KIỂM TRA SẢN PHẨM, GIÁ VÀ TỒN KHO
                decimal finalAmount = 0m;
                int totalItems = 0;

                var checkedItems = new List<(
                    int ProductId,
                    int Quantity,
                    decimal UnitPrice,
                    decimal LineTotal)>();

                foreach (var item in request.Items)
                {
                    if (item.ProductId <= 0 || item.Quantity <= 0)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            success = false,
                            message = "Mã sản phẩm và số lượng phải hợp lệ!"
                        });
                    }

                    const string sqlProduct = @"
                SELECT ProductName, Price, StockQuantity
                FROM Products WITH (UPDLOCK, ROWLOCK)
                WHERE ProductId = @ProductId;";

                    await using var cmdProduct =
                        new SqlCommand(sqlProduct, connection, transaction);

                    cmdProduct.Parameters.Add(
                        "@ProductId", SqlDbType.Int).Value = item.ProductId;

                    string productName;
                    decimal unitPrice;
                    int stockQuantity;

                    await using (var reader =
                        await cmdProduct.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                        {
                            await reader.CloseAsync();
                            await transaction.RollbackAsync();

                            return BadRequest(new
                            {
                                success = false,
                                message =
                                    $"Không tìm thấy sản phẩm ID = {item.ProductId}!"
                            });
                        }

                        productName = reader.GetString(
                            reader.GetOrdinal("ProductName"));

                        unitPrice = reader.GetDecimal(
                            reader.GetOrdinal("Price"));

                        stockQuantity = reader.GetInt32(
                            reader.GetOrdinal("StockQuantity"));
                    }

                    if (stockQuantity < item.Quantity)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            success = false,
                            message =
                                $"Sản phẩm \"{productName}\" chỉ còn " +
                                $"{stockQuantity} sản phẩm trong kho!"
                        });
                    }

                    // Giá lấy từ SQL Server, không tin giá gửi từ Client.
                    decimal lineTotal = unitPrice * item.Quantity;

                    finalAmount += lineTotal;
                    totalItems += item.Quantity;

                    checkedItems.Add((
                        item.ProductId,
                        item.Quantity,
                        unitPrice,
                        lineTotal));
                }

                // 3. TRỪ TỒN KHO
                foreach (var item in checkedItems)
                {
                    const string sqlUpdateStock = @"
                UPDATE Products
                SET StockQuantity = StockQuantity - @Quantity
                WHERE ProductId = @ProductId
                  AND StockQuantity >= @Quantity;";

                    await using var cmdStock =
                        new SqlCommand(
                            sqlUpdateStock, connection, transaction);

                    cmdStock.Parameters.Add(
                        "@Quantity", SqlDbType.Int).Value = item.Quantity;

                    cmdStock.Parameters.Add(
                        "@ProductId", SqlDbType.Int).Value = item.ProductId;

                    int affected = await cmdStock.ExecuteNonQueryAsync();

                    if (affected != 1)
                    {
                        await transaction.RollbackAsync();

                        return Conflict(new
                        {
                            success = false,
                            message =
                                "Tồn kho đã thay đổi. Vui lòng kiểm tra giỏ hàng và thử lại."
                        });
                    }
                }

                // 4. TÍNH ĐIỂM THƯỞNG: 10.000 đồng = 1 điểm
                int rewardPoints = (int)Math.Floor(finalAmount / 10000m);

                // 5. THÊM HÓA ĐƠN
                string orderCode =
                    $"HD-{DateTime.Now:yyyyMMddHHmmssfff}";

                const string sqlInsertOrder = @"
            INSERT INTO Orders
                (OrderCode, CreatedDate, CashierUsername, CustomerId,
                 TotalItems, FinalAmount, RewardPoints)
            OUTPUT INSERTED.OrderId
            VALUES
                (@OrderCode, @CreatedDate, @CashierUsername, @CustomerId,
                 @TotalItems, @FinalAmount, @RewardPoints);";

                int orderId;

                await using (var cmdOrder =
                    new SqlCommand(sqlInsertOrder, connection, transaction))
                {
                    cmdOrder.Parameters.Add(
                        "@OrderCode", SqlDbType.VarChar, 50).Value = orderCode;

                    cmdOrder.Parameters.Add(
                        "@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;

                    cmdOrder.Parameters.Add(
                        "@CashierUsername", SqlDbType.NVarChar, 50).Value =
                        cashierUsername;
                    cmdOrder.Parameters.Add(
                        "@CustomerId", SqlDbType.Int).Value = customerId;

                    cmdOrder.Parameters.Add(
                        "@TotalItems", SqlDbType.Int).Value = totalItems;

                    var amountParameter =
                        cmdOrder.Parameters.Add("@FinalAmount", SqlDbType.Decimal);
                    amountParameter.Precision = 18;
                    amountParameter.Scale = 2;
                    amountParameter.Value = finalAmount;

                    cmdOrder.Parameters.Add(
                        "@RewardPoints", SqlDbType.Int).Value = rewardPoints;

                    orderId = Convert.ToInt32(
                        await cmdOrder.ExecuteScalarAsync());
                }

                // 6. THÊM CHI TIẾT HÓA ĐƠN
                const string sqlInsertDetail = @"
            INSERT INTO OrderDetails
                (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
            VALUES
                (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal);";

                foreach (var item in checkedItems)
                {
                    await using var cmdDetail =
                        new SqlCommand(
                            sqlInsertDetail, connection, transaction);

                    cmdDetail.Parameters.Add(
                        "@OrderId", SqlDbType.Int).Value = orderId;

                    cmdDetail.Parameters.Add(
                        "@ProductId", SqlDbType.Int).Value = item.ProductId;

                    cmdDetail.Parameters.Add(
                        "@Quantity", SqlDbType.Int).Value = item.Quantity;

                    var priceParameter =
                        cmdDetail.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                    priceParameter.Precision = 18;
                    priceParameter.Scale = 2;
                    priceParameter.Value = item.UnitPrice;

                    var lineParameter =
                        cmdDetail.Parameters.Add("@LineTotal", SqlDbType.Decimal);
                    lineParameter.Precision = 18;
                    lineParameter.Scale = 2;
                    lineParameter.Value = item.LineTotal;

                    await cmdDetail.ExecuteNonQueryAsync();
                }

                // 7. CỘNG ĐIỂM THƯỞNG CHO KHÁCH
                const string sqlUpdatePoints = @"
            UPDATE Customers
            SET RewardPoints = RewardPoints + @RewardPoints
            WHERE CustomerId = @CustomerId;";

                await using (var cmdPoints =
                    new SqlCommand(sqlUpdatePoints, connection, transaction))
                {
                    cmdPoints.Parameters.Add(
                        "@RewardPoints", SqlDbType.Int).Value = rewardPoints;

                    cmdPoints.Parameters.Add(
                        "@CustomerId", SqlDbType.Int).Value = customerId;

                    int affected = await cmdPoints.ExecuteNonQueryAsync();

                    if (affected != 1)
                    {
                        throw new InvalidOperationException(
                            "Không thể cập nhật điểm thưởng khách hàng.");
                    }
                }

                // 8. XÁC NHẬN TOÀN BỘ GIAO DỊCH
                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Thanh toán hóa đơn POS thành công!",
                    orderId,
                    orderCode,
                    totalItems,
                    finalAmount,
                    rewardPointsAdded = rewardPoints
                });
            }
            catch (Exception ex)
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch
                {
                    // Giữ lại lỗi gốc nếu Rollback cũng gặp lỗi.
                }

                return StatusCode(500, new
                {
                    success = false,
                    message = "Giao dịch thanh toán thất bại; đã yêu cầu Rollback.",
                    errorDetail = ex.Message
                });
            }
        }
        // =====================================================
        // GET: /api/orders
        // Dùng sau này cho FormQuickReport
        // =====================================================

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .OrderByDescending(o => o.CreatedDate)
                .Select(o => new OrderListDto
                {
                    OrderId = o.OrderId,
                    OrderCode = o.OrderCode,
                    CreatedDate = o.CreatedDate,
                    CashierUsername = o.CashierUsername,
                    CustomerName = o.Customer != null
                        ? o.Customer.CustomerName
                        : "Khách vãng lai",
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount
                })
                .ToListAsync();

            return Ok(orders);
        }


        // =====================================================
        // GET: /api/orders/{id}
        // =====================================================


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Where(o => o.OrderId == id)
                .Select(o => new
                {
                    o.OrderId,
                    o.OrderCode,
                    o.CreatedDate,
                    o.CashierUsername,
                    o.TotalItems,
                    o.FinalAmount,

                    CustomerName = o.Customer != null
                        ? o.Customer.CustomerName
                        : "Khách vãng lai",

                    OrderDetails = o.OrderDetails.Select(d => new
                    {
                        d.OrderDetailId,
                        d.OrderId,
                        d.ProductId,
                        d.Quantity,
                        d.UnitPrice,
                        d.LineTotal,

                        Product = d.Product == null
                            ? null
                            : new
                            {
                                d.Product.ProductId,
                                d.Product.ProductName
                            }
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy đơn hàng."
                });
            }

            return Ok(order);
        }

    }
        // =========================================================
        // DTO CHECKOUT
        // =========================================================

        public class CheckoutRequest
    {
        public string CashierUsername { get; set; }
            = string.Empty;

        public string CustomerPhone { get; set; }
            = string.Empty;

        public List<CheckoutItemRequest> Items { get; set; }
            = new();
    }


    public class CheckoutItemRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}