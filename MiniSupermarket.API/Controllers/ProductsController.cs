using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;
using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =========================================
        // GET: api/products
        // LẤY TẤT CẢ SẢN PHẨM CÓ PHÂN TRANG
        // =========================================
        // GET: api/products/paged?keyword=sua&pageNumber=1&pageSize=5

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] int? categoryId = null)
        {
            // 1. Kiểm tra tham số phân trang
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1 || pageSize > 50)
                pageSize = 5;

            // 2. Truy vấn sản phẩm và danh mục
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 3. Lọc theo từ khóa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();

                query = query.Where(p =>
                    p.ProductName.ToLower().Contains(search) ||
                    p.Barcode.Contains(search));
            }

            // 4. Lọc theo danh mục nếu người dùng đã chọn
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            // 5. Đếm tổng sản phẩm sau khi áp dụng bộ lọc
            int totalItems = await query.CountAsync();

            // 6. Tính tổng số trang
            int totalPages = (int)Math.Ceiling(
                (double)totalItems / pageSize);

            // 7. Lấy sản phẩm của trang hiện tại
            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId,
                    Barcode = p.Barcode,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category != null
                        ? p.Category.CategoryName
                        : string.Empty
                })
                .ToListAsync();

            // 8. Trả kết quả phân trang
            var response = new PagedResult<ProductItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(response);
        }

        // =========================================
        // GET: api/products
        // LẤY TẤT CẢ SẢN PHẨM 
        // =========================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return Ok(products);
        }

        // =========================================
        // GET: api/products/5
        // LẤY SẢN PHẨM THEO ID
        // =========================================

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            return Ok(product);
        }

        // =========================================
        // GET: api/products/search?keyword=...
        // TÌM KIẾM
        // =========================================

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetProducts();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.ProductName.Contains(keyword) ||
                    p.Barcode.Contains(keyword))
                .ToListAsync();

            return Ok(products);
        }

        // =========================================
        // POST: api/products
        // THÊM SẢN PHẨM
        // =========================================

        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<ActionResult<Product>> CreateProduct(
            Product product)
        {
            // Kiểm tra Barcode trùng
            bool barcodeExists =
                await _context.Products
                    .AnyAsync(p => p.Barcode == product.Barcode);

            if (barcodeExists)
            {
                return BadRequest(new
                {
                    message = "Mã Barcode đã tồn tại."
                });
            }

            // Kiểm tra Category
            bool categoryExists =
                await _context.Categories
                    .AnyAsync(c => c.CategoryId == product.CategoryId);

            if (!categoryExists)
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại."
                });
            }

            // Không cho client tự truyền ID
            product.ProductId = 0;

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.ProductId },
                product);
        }

        // =========================================
        // PUT: api/products/5
        // CẬP NHẬT SẢN PHẨM
        // =========================================

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new
                {
                    message = "ID sản phẩm không khớp."
                });
            }

            var existingProduct =
                await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            // Kiểm tra Barcode trùng với sản phẩm khác
            bool barcodeExists =
                await _context.Products.AnyAsync(p =>
                    p.Barcode == product.Barcode &&
                    p.ProductId != id);

            if (barcodeExists)
            {
                return BadRequest(new
                {
                    message = "Mã Barcode đã tồn tại."
                });
            }

            // Kiểm tra Category
            bool categoryExists =
                await _context.Categories
                    .AnyAsync(c => c.CategoryId == product.CategoryId);

            if (!categoryExists)
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại."
                });
            }

            existingProduct.Barcode = product.Barcode;
            existingProduct.ProductName = product.ProductName;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================================
        // DELETE: api/products/5
        // XÓA SẢN PHẨM
        // =========================================

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =
                await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}