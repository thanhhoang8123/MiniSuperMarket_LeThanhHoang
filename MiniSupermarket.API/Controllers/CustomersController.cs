using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng!" });

            return Ok(customer);
        }

        // GET: api/customers/search?keyword=Nguyễn
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });

            var result = await _context.Customers
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .AsNoTracking()
                .ToListAsync();

            return Ok(result);
        }

        // GET: api/customers/paged?pageNumber=1&pageSize=5&keyword=nguyen
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedCustomers(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] string? keyword = null)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (pageSize < 1 || pageSize > 50)
                pageSize = 5;

            // Tạo truy vấn và lọc theo tên hoặc số điện thoại nếu có từ khóa.
            var query = _context.Customers
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim();

                query = query.Where(c =>
                    c.CustomerName.Contains(search) ||
                    c.PhoneNumber.Contains(search));
            }

            // Đếm tổng số khách hàng sau khi lọc.
            int totalItems = await query.CountAsync();

            // Sắp xếp điểm tích lũy giảm dần, rồi đến mã khách hàng tăng dần.
            var orderedQuery = query
                .OrderByDescending(c => c.RewardPoints)
                .ThenBy(c => c.CustomerId);

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Nếu trang yêu cầu vượt quá tổng số trang, đưa về trang cuối.
            if (totalPages > 0 && pageNumber > totalPages)
                pageNumber = totalPages;

            var customers = await orderedQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = customers
            });
        }

        // POST: api/customers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customer);
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updateCustomer)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng!" });

            customer.CustomerName = updateCustomer.CustomerName;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.Address = updateCustomer.Address;
            customer.RewardPoints = updateCustomer.RewardPoints;
            customer.MembershipRank = updateCustomer.MembershipRank;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng!" });

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
