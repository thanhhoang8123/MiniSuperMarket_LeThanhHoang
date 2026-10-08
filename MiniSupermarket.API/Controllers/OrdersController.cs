using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
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
                    message = "Giỏ hàng đang trống!"
                });
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // =================================================
                // 1. TÌM KHÁCH HÀNG
                // =================================================

                Customer? customer = null;

                if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
                {
                    customer = await _context.Customers
                        .FirstOrDefaultAsync(
                            c => c.PhoneNumber == request.CustomerPhone.Trim());

                    if (customer == null)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            message = "Không tìm thấy khách hàng với số điện thoại này!"
                        });
                    }
                }
                else
                {
                    // Khách vãng lai
                    customer = await _context.Customers
                        .FirstOrDefaultAsync(
                            c => c.PhoneNumber == "0000000000");

                    if (customer == null)
                    {
                        customer = new Customer
                        {
                            CustomerName = "Khách vãng lai",
                            PhoneNumber = "0000000000",
                            Address = "",
                            RewardPoints = 0,
                            MembershipRank = "Chuẩn"
                        };

                        _context.Customers.Add(customer);
                        await _context.SaveChangesAsync();
                    }
                }

                // =================================================
                // 2. KIỂM TRA SẢN PHẨM + TỒN KHO
                // =================================================

                decimal finalAmount = 0;
                int totalItems = 0;

                var orderDetails = new List<OrderDetail>();

                foreach (var item in request.Items)
                {
                    if (item.Quantity <= 0)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            message = "Số lượng sản phẩm phải lớn hơn 0!"
                        });
                    }

                    var product = await _context.Products
                        .FirstOrDefaultAsync(
                            p => p.ProductId == item.ProductId);

                    if (product == null)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            message =
                                $"Không tìm thấy sản phẩm ID = {item.ProductId}!"
                        });
                    }

                    // Kiểm tra tồn kho từ DATABASE
                    if (product.StockQuantity < item.Quantity)
                    {
                        await transaction.RollbackAsync();

                        return BadRequest(new
                        {
                            message =
                                $"Sản phẩm \"{product.ProductName}\" chỉ còn " +
                                $"{product.StockQuantity} sản phẩm trong kho!"
                        });
                    }

                    // Không lấy UnitPrice từ Client làm giá chính thức.
                    // Lấy giá hiện tại trong DATABASE.
                    decimal unitPrice = product.Price;

                    decimal lineTotal =
                        unitPrice * item.Quantity;

                    totalItems += item.Quantity;
                    finalAmount += lineTotal;

                    // Trừ kho
                    product.StockQuantity -= item.Quantity;

                    orderDetails.Add(
                        new OrderDetail
                        {
                            ProductId = product.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = unitPrice,
                            LineTotal = lineTotal
                        });
                }

                // =================================================
                // 3. TẠO ORDER
                // =================================================

                string orderCode =
                    $"HD-{DateTime.Now:yyyyMMddHHmmssfff}";

                var order = new Order
                {
                    OrderCode = orderCode,
                    CreatedDate = DateTime.Now,
                    CashierUsername = request.CashierUsername,
                    CustomerId = customer.CustomerId,
                    TotalItems = totalItems,
                    FinalAmount = finalAmount,
                    RewardPoints = 0
                };

                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

                // =================================================
                // 4. GÁN ORDER ID CHO ORDER DETAILS
                // =================================================

                foreach (var detail in orderDetails)
                {
                    detail.OrderId = order.OrderId;
                }

                _context.OrderDetails.AddRange(orderDetails);

                await _context.SaveChangesAsync();

                // =================================================
                // 5. COMMIT TRANSACTION
                // =================================================

                await transaction.CommitAsync();

                return Ok(new
                {
                    success = true,
                    message = "Thanh toán thành công!",
                    orderId = order.OrderId,
                    orderCode = order.OrderCode,
                    totalItems = order.TotalItems,
                    finalAmount = order.FinalAmount
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return StatusCode(500, new
                {
                    success = false,
                    message = "Thanh toán thất bại!",
                    error = ex.Message
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
                .Include(o => o.Customer)
                .OrderByDescending(o => o.CreatedDate)
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
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy hóa đơn!"
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