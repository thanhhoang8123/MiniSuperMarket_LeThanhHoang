using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Models;

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
        // GET: api/products/paged?keyword=sua&pageNumber=1&pageSize=5
        // =========================================
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(
            [FromQuery] string? keyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] int? categoryId = null)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1 || pageSize > 50)
                pageSize = 5;

            // Tối ưu truy vấn: .Include() nạp nhóm hàng liên quan, .AsNoTracking() giải phóng bộ nhớ RAM (chỉ đọc)
            var query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .AsQueryable();

            // 1. TÌM KIẾM GẦN ĐÚNG BẰNG LINQ .Contains()
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p =>
                    p.ProductName.ToLower().Contains(search) ||
                    p.Barcode.Contains(search));
            }

            // 2. LỌC THEO NHÓM HÀNG BẰNG LINQ .Where()
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            // Đếm tổng số mặt hàng thỏa điều kiện lọc
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // 3. PHÂN TRANG DỮ LIỆU MÁY CHỦ BẰNG .Skip() VÀ .Take()
            var items = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
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

            var response = new PagedResult<ProductDto>
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
        // =========================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Select(p => new ProductDto
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

            return Ok(products);
        }

        // =========================================
        // GET: api/products/5
        // =========================================
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p => p.ProductId == id)
                .Select(p => new ProductDto
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
                .FirstOrDefaultAsync();

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
        // =========================================
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> SearchProducts(
            [FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await GetProducts();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .Where(p =>
                    p.ProductName.Contains(keyword) ||
                    p.Barcode.Contains(keyword))
                .Select(p => new ProductDto
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

            return Ok(products);
        }

        // =========================================
        // POST: api/products
        // THAO TÁC THÊM MỚI SẢN PHẨM QUA EF CORE (Create: Add & SaveChangesAsync)
        // =========================================
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<ActionResult<ProductDto>> CreateProduct(
            [FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool barcodeExists = await _context.Products
                .AnyAsync(p => p.Barcode == dto.Barcode);

            if (barcodeExists)
            {
                return BadRequest(new
                {
                    message = "Mã Barcode đã tồn tại."
                });
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == dto.CategoryId);

            if (category == null)
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại."
                });
            }

            var product = new Product
            {
                Barcode = dto.Barcode,
                ProductName = dto.ProductName,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var productDto = new ProductDto
            {
                ProductId = product.ProductId,
                Barcode = product.Barcode,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = category.CategoryName
            };

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.ProductId },
                productDto);
        }

        // =========================================
        // PUT: api/products/{id}
        // THAO TÁC CẬP NHẬT SẢN PHẨM QUA EF CORE (Update: SaveChangesAsync)
        // =========================================
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.ProductId)
            {
                return BadRequest(new
                {
                    message = "ID sản phẩm không khớp."
                });
            }

            var existingProduct = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existingProduct == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            bool barcodeExists = await _context.Products.AnyAsync(p =>
                p.Barcode == dto.Barcode &&
                p.ProductId != id);

            if (barcodeExists)
            {
                return BadRequest(new
                {
                    message = "Mã Barcode đã tồn tại."
                });
            }

            bool categoryExists = await _context.Categories
                .AnyAsync(c => c.CategoryId == dto.CategoryId);

            if (!categoryExists)
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại."
                });
            }

            existingProduct.Barcode = dto.Barcode;
            existingProduct.ProductName = dto.ProductName;
            existingProduct.Price = dto.Price;
            existingProduct.StockQuantity = dto.StockQuantity;
            existingProduct.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =========================================
        // DELETE: api/products/{id}
        // THAO TÁC XÓA SẢN PHẨM QUA EF CORE (Delete: Remove & SaveChangesAsync)
        // =========================================
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            bool isUsedInOrders = await _context.OrderDetails
                .AnyAsync(detail => detail.ProductId == id);
            if (isUsedInOrders)
            {
                return Conflict(new
                {
                    message = "Không thể xóa sản phẩm đã xuất hiện trong hóa đơn. Hãy ngừng kinh doanh sản phẩm thay vì xóa."
                });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

