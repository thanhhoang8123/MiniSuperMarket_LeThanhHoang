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
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/customers/1 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng!" });

            return Ok(customer);
        }

        // GET: api/customers/by-phone/0901234567
        // Tìm thông tin khách hàng và số điểm thưởng tích lũy theo số điện thoại
        [HttpGet("by-phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return BadRequest(new { success = false, message = "Số điện thoại không hợp lệ!" });

            var customer = await _context.Customers
                .AsNoTracking()
                .Where(c => c.PhoneNumber == phone.Trim())
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound(new { success = false, message = "Không tìm thấy khách hàng với số điện thoại này!" });

            return Ok(new { success = true, data = customer });
        }

        // GET: api/customers/search?keyword=Nguyễn
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return BadRequest(new { message = "Vui lòng nhập từ khóa!" });

            var result = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
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

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (totalPages > 0 && pageNumber > totalPages)
                pageNumber = totalPages;

            var customers = await query
                .OrderByDescending(c => c.RewardPoints)
                .ThenBy(c => c.CustomerId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    RewardPoints = c.RewardPoints,
                    MembershipRank = c.MembershipRank
                })
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
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Create([FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = new Customer
            {
                CustomerName = dto.CustomerName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                RewardPoints = 0,
                MembershipRank = "Chuẩn",
                RegisteredAt = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var customerDto = new CustomerDto
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                RewardPoints = customer.RewardPoints,
                MembershipRank = customer.MembershipRank
            };

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.CustomerId },
                customerDto);
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] CustomerUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
                return NotFound(new { message = "Không tìm thấy khách hàng!" });

            customer.CustomerName = dto.CustomerName;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.Address = dto.Address;
            if (User.IsInRole("Admin"))
            {
                customer.RewardPoints = dto.RewardPoints;
                customer.MembershipRank = dto.MembershipRank;
            }

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

