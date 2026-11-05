using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

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
        // THANH TOÁN HÓA ĐƠN & ĐỔI ĐIỂM THƯỞNG BẰNG SQLTRANSACTION (ACID)
        // =====================================================
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            if (request.Items == null || request.Items.Count == 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Giỏ hàng đang trống!"
                });
            }

            // Lấy username từ JWT đã được xác thực, dự phòng bằng thông tin request nếu rỗng
            string cashierUsername = User.Identity?.Name ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cashierUsername))
            {
                cashierUsername = request.CashierUsername ?? string.Empty;
            }

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
                ?? throw new InvalidOperationException("Chưa cấu hình DefaultConnection.");

            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // KHỞI TẠO GIAO DỊCH SqlTransaction ĐẢM BẢO TÍNH NGUYÊN TỐ TOÀN VẸN DỮ LIỆU (ACID)
            // Lưu hóa đơn, trừ tồn kho, đổi điểm trừ tiền và tích điểm nằm trọn trong 1 Transaction duy nhất
            await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {  
                // 1. TÌM KHÁCH HÀNG & LẤY ĐIỂM TÍCH LŨY HIỆN CÓ
                int customerId;
                int existingRewardPoints = 0;

                if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
                {
                    const string sqlCustomer = @"
                    SELECT CustomerId, RewardPoints
                    FROM Customers WITH (UPDLOCK, ROWLOCK)
                    WHERE PhoneNumber = @PhoneNumber;";

                    await using var cmdCustomer = new SqlCommand(sqlCustomer, connection, transaction);
                    cmdCustomer.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value = request.CustomerPhone.Trim();

                    await using var readerCustomer = await cmdCustomer.ExecuteReaderAsync();
                    if (!await readerCustomer.ReadAsync())
                    {
                        await readerCustomer.CloseAsync();
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            success = false,
                            message = "Không tìm thấy khách hàng với số điện thoại này!"
                        });
                    }

                    customerId = readerCustomer.GetInt32(readerCustomer.GetOrdinal("CustomerId"));
                    existingRewardPoints = readerCustomer.GetInt32(readerCustomer.GetOrdinal("RewardPoints"));
                    await readerCustomer.CloseAsync();
                }
                else
                {
                    // Khách vãng lai
                    const string sqlGuest = @"
                    SELECT CustomerId, RewardPoints
                    FROM Customers
                    WHERE PhoneNumber = @PhoneNumber;";

                    await using var cmdGuest = new SqlCommand(sqlGuest, connection, transaction);
                    cmdGuest.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value = "0000000000";

                    await using var readerGuest = await cmdGuest.ExecuteReaderAsync();
                    if (await readerGuest.ReadAsync())
                    {
                        customerId = readerGuest.GetInt32(readerGuest.GetOrdinal("CustomerId"));
                        existingRewardPoints = 0;
                        await readerGuest.CloseAsync();
                    }
                    else
                    {
                        await readerGuest.CloseAsync();
                        const string sqlInsertGuest = @"
                        INSERT INTO Customers
                            (CustomerName, PhoneNumber, Address, RewardPoints, MembershipRank)
                        OUTPUT INSERTED.CustomerId
                        VALUES
                            (@CustomerName, @PhoneNumber, @Address, 0, @MembershipRank);";

                        await using var cmdInsertGuest = new SqlCommand(sqlInsertGuest, connection, transaction);
                        cmdInsertGuest.Parameters.Add("@CustomerName", SqlDbType.NVarChar, 100).Value = "Khách vãng lai";
                        cmdInsertGuest.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value = "0000000000";
                        cmdInsertGuest.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = "";
                        cmdInsertGuest.Parameters.Add("@MembershipRank", SqlDbType.NVarChar, 50).Value = "Chuẩn";

                        customerId = Convert.ToInt32(await cmdInsertGuest.ExecuteScalarAsync());
                        existingRewardPoints = 0;
                    }
                }

                // 2. KIỂM TRA SẢN PHẨM, GIÁ VÀ TỒN KHO
                decimal subTotal = 0m;
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

                    await using var cmdProduct = new SqlCommand(sqlProduct, connection, transaction);
                    cmdProduct.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;

                    string productName;
                    decimal unitPrice;
                    int stockQuantity;

                    await using (var reader = await cmdProduct.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                        {
                            await reader.CloseAsync();
                            await transaction.RollbackAsync();

                            return BadRequest(new
                            {
                                success = false,
                                message = $"Không tìm thấy sản phẩm ID = {item.ProductId}!"
                            });
                        }

                        productName = reader.GetString(reader.GetOrdinal("ProductName"));
                        unitPrice = reader.GetDecimal(reader.GetOrdinal("Price"));
                        stockQuantity = reader.GetInt32(reader.GetOrdinal("StockQuantity"));
                    }

                    // TỰ ĐỘNG HOÀN TÁC (RollbackAsync) KHI KHO HẾT HÀNG
                    if (stockQuantity < item.Quantity)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new
                        {
                            success = false,
                            message = $"Sản phẩm \"{productName}\" chỉ còn {stockQuantity} sản phẩm trong kho!"
                        });
                    }

                    // Giá bán lấy trực tiếp từ SQL Server
                    decimal lineTotal = unitPrice * item.Quantity;
                    subTotal += lineTotal;
                    totalItems += item.Quantity;

                    checkedItems.Add((
                        item.ProductId,
                        item.Quantity,
                        unitPrice,
                        lineTotal));
                }

                // =========================================================
                // 3. ĐỔI ĐIỂM THƯỞNG TRỪ TIỀN HÓA ĐƠN (LOYALTY POINTS REDEMPTION)
                // Quy đổi: 1 điểm = 1.000 VNĐ (ví dụ: dùng 50 điểm trừ 50.000 VNĐ)
                // =========================================================
                decimal discountAmount = 0m;
                int pointsToRedeem = request.PointsToRedeem;

                if (pointsToRedeem > 0)
                {
                    if (string.IsNullOrWhiteSpace(request.CustomerPhone))
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new
                        {
                            success = false,
                            message = "Khách vãng lai không có điểm thưởng tích lũy để đổi!"
                        });
                    }

                    if (pointsToRedeem > existingRewardPoints)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new
                        {
                            success = false,
                            message = $"Khách hàng chỉ có {existingRewardPoints} điểm, không đủ để đổi {pointsToRedeem} điểm!"
                        });
                    }

                    int affordablePoints = (int)Math.Min(int.MaxValue, decimal.Floor(subTotal / 1000m));
                    if (pointsToRedeem > affordablePoints)
                    {
                        await transaction.RollbackAsync();
                        return BadRequest(new
                        {
                            success = false,
                            message = $"Hóa đơn hiện tại chỉ có thể đổi tối đa {affordablePoints} điểm (1 điểm = 1.000 đ)."
                        });
                    }

                    discountAmount = pointsToRedeem * 1000m;
                }

                decimal finalAmount = subTotal - discountAmount;

                // 4. TRỪ TỒN KHO TRONG TRANSACTION
                foreach (var item in checkedItems)
                {
                    const string sqlUpdateStock = @"
                    UPDATE Products
                    SET StockQuantity = StockQuantity - @Quantity
                    WHERE ProductId = @ProductId
                      AND StockQuantity >= @Quantity;";

                    await using var cmdStock = new SqlCommand(sqlUpdateStock, connection, transaction);
                    cmdStock.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                    cmdStock.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;

                    int affected = await cmdStock.ExecuteNonQueryAsync();
                    if (affected != 1)
                    {
                        await transaction.RollbackAsync();
                        return Conflict(new
                        {
                            success = false,
                            message = "Tồn kho đã thay đổi. Vui lòng kiểm tra giỏ hàng và thử lại."
                        });
                    }
                }

                // 5. TÍNH ĐIỂM THƯỞNG PHÁT SINH TỪ HÓA ĐƠN HIỆN TẠI (10.000 VNĐ = 1 điểm)
                int rewardPointsEarned = (int)Math.Floor(finalAmount / 10000m);

                // 6. THÊM HÓA ĐƠN VỚI SỐ TIỀN FINALAMOUNT ĐÃ TRỪ ĐIỂM
                string orderCode = $"HD-{DateTime.Now:yyyyMMddHHmmssfff}";

                const string sqlInsertOrder = @"
                INSERT INTO Orders
                    (OrderCode, CreatedDate, CashierUsername, CustomerId,
                     TotalItems, FinalAmount, RewardPoints)
                OUTPUT INSERTED.OrderId
                VALUES
                    (@OrderCode, @CreatedDate, @CashierUsername, @CustomerId,
                     @TotalItems, @FinalAmount, @RewardPoints);";

                int orderId;
                await using (var cmdOrder = new SqlCommand(sqlInsertOrder, connection, transaction))
                {
                    cmdOrder.Parameters.Add("@OrderCode", SqlDbType.VarChar, 50).Value = orderCode;
                    cmdOrder.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmdOrder.Parameters.Add("@CashierUsername", SqlDbType.NVarChar, 50).Value = cashierUsername;
                    cmdOrder.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
                    cmdOrder.Parameters.Add("@TotalItems", SqlDbType.Int).Value = totalItems;

                    var amountParameter = cmdOrder.Parameters.Add("@FinalAmount", SqlDbType.Decimal);
                    amountParameter.Precision = 18;
                    amountParameter.Scale = 2;
                    amountParameter.Value = finalAmount;

                    cmdOrder.Parameters.Add("@RewardPoints", SqlDbType.Int).Value = rewardPointsEarned;

                    orderId = Convert.ToInt32(await cmdOrder.ExecuteScalarAsync());
                }

                // 7. THÊM CHI TIẾT HÓA ĐƠN
                const string sqlInsertDetail = @"
                INSERT INTO OrderDetails
                    (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
                VALUES
                    (@OrderId, @ProductId, @Quantity, @UnitPrice, @LineTotal);";

                foreach (var item in checkedItems)
                {
                    await using var cmdDetail = new SqlCommand(sqlInsertDetail, connection, transaction);
                    cmdDetail.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderId;
                    cmdDetail.Parameters.Add("@ProductId", SqlDbType.Int).Value = item.ProductId;
                    cmdDetail.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;

                    var priceParameter = cmdDetail.Parameters.Add("@UnitPrice", SqlDbType.Decimal);
                    priceParameter.Precision = 18;
                    priceParameter.Scale = 2;
                    priceParameter.Value = item.UnitPrice;

                    var lineParameter = cmdDetail.Parameters.Add("@LineTotal", SqlDbType.Decimal);
                    lineParameter.Precision = 18;
                    lineParameter.Scale = 2;
                    lineParameter.Value = item.LineTotal;

                    await cmdDetail.ExecuteNonQueryAsync();
                }

                // 8. CẬP NHẬT ĐỒNG THỜI TRỪ ĐIỂM VÀ CỘNG ĐIỂM TRONG BẢNG CUSTOMERS CÙNG SqlTransaction
                const string sqlUpdatePoints = @"
                UPDATE Customers
                SET RewardPoints = RewardPoints - @PointsRedeemed + @PointsEarned
                WHERE CustomerId = @CustomerId AND RewardPoints >= @PointsRedeemed;";

                await using (var cmdPoints = new SqlCommand(sqlUpdatePoints, connection, transaction))
                {
                    cmdPoints.Parameters.Add("@PointsRedeemed", SqlDbType.Int).Value = pointsToRedeem;
                    cmdPoints.Parameters.Add("@PointsEarned", SqlDbType.Int).Value = rewardPointsEarned;
                    cmdPoints.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;

                    int affected = await cmdPoints.ExecuteNonQueryAsync();
                    if (affected != 1)
                    {
                        await transaction.RollbackAsync();
                        return Conflict(new
                        {
                            success = false,
                            message = "Điểm tích lũy của khách hàng không đủ hoặc đã bị thay đổi."
                        });
                    }
                }

                // 9. XÁC NHẬN HOÀN TẤT TOÀN BỘ GIAO DỊCH (CommitAsync)
                // Hóa đơn + Chi tiết + Trừ kho + Trừ điểm đổi thưởng + Cộng điểm mới cùng có hiệu lực đồng thời
                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Thanh toán hóa đơn POS thành công!",
                    orderId,
                    orderCode,
                    totalItems,
                    subTotal,
                    discountAmount,
                    pointsRedeemed = pointsToRedeem,
                    finalAmount,
                    rewardPointsAdded = rewardPointsEarned
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
                    // Bỏ qua nếu giao dịch đã rollback
                }

                return StatusCode(500, new
                {
                    success = false,
                    message = "Giao dịch thanh toán thất bại; đã yêu cầu Rollback an toàn.",
                    errorDetail = ex.Message
                });
            }
        }

        // =====================================================
        // GET: /api/orders
        // =====================================================
        [HttpGet]
        [Authorize(Roles = "Admin,Cashier")]
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
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount
                })
                .ToListAsync();

            return Ok(orders);
        }

        // GET: /api/orders/paged?pageNumber=1&pageSize=5
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = pageSize is < 1 or > 50 ? 5 : pageSize;

            var query = _context.Orders.AsNoTracking();
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            pageNumber = totalPages == 0 ? 1 : Math.Min(pageNumber, totalPages);

            var items = await query
                .OrderByDescending(o => o.CreatedDate)
                .ThenByDescending(o => o.OrderId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderListDto
                {
                    OrderId = o.OrderId,
                    OrderCode = o.OrderCode,
                    CreatedDate = o.CreatedDate,
                    CashierUsername = o.CashierUsername,
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    TotalItems = o.TotalItems,
                    FinalAmount = o.FinalAmount
                })
                .ToListAsync();

            return Ok(new PagedResult<OrderListDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // =====================================================
        // GET: /api/orders/{id}
        // =====================================================
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
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
                    CustomerName = o.Customer != null ? o.Customer.CustomerName : "Khách vãng lai",
                    OrderDetails = o.OrderDetails.Select(d => new
                    {
                        d.OrderDetailId,
                        d.OrderId,
                        d.ProductId,
                        d.Quantity,
                        d.UnitPrice,
                        d.LineTotal,
                        Product = d.Product == null ? null : new
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
}


