using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;

        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1. Tìm tài khoản theo Username (hỗ trợ cả biến thể ware01 / warehouse01)
            string uname = request.Username?.Trim() ?? string.Empty;
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == uname ||
                    (uname == "warehouse01" && u.Username == "ware01") ||
                    (uname == "warehouse" && u.Username == "ware01") ||
                    (uname == "cashier" && u.Username == "cashier01") ||
                    (uname == "admin" && u.Username == "admin01"));

            // 2. Không tồn tại tài khoản
            if (user == null)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // 3. Tài khoản bị khóa
            if (!user.IsActive)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Tài khoản đã bị khóa hoặc ngưng hoạt động!"
                });
            }

            // 4. Kiểm tra mật khẩu
            if (user.PasswordHash != request.Password)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "Sai tài khoản hoặc mật khẩu!"
                });
            }

            // 5. Đăng nhập thành công
            var token = GenerateJwtToken(user.Username, user.Role);

            return Ok(new
            {
                success = true,
                token = token,
                role = user.Role
            });
        }

        // =====================================================
        // HÀM KHỞI TẠO VÀ KÝ MÃ JWT TOKEN
        // =====================================================
        private string GenerateJwtToken(
            string username,
            string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Khóa bí mật đối xứng lấy từ appsettings.json
            var key = Encoding.ASCII.GetBytes(
                _configuration["JwtSettings:Secret"]
                ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!");

            // Cấu hình gói thông tin Token (Claims, thời hạn, chữ ký số)
            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        // 1. Nhúng tên định danh tài khoản (Username)
                        new Claim(
                            ClaimTypes.Name,
                            username),

                        // 2. Nhúng vai trò phân quyền (Role: Admin, Cashier, Warehouse)
                        new Claim(
                            ClaimTypes.Role,
                            role)
                    }),

                    // Thời hạn hiệu lực của Token (2 giờ kể từ lúc cấp)
                    Expires = DateTime.UtcNow.AddHours(2),

                    // Ký số mã hóa bằng khóa đối xứng SymmetricSecurityKey và thuật toán HMAC-SHA256
                    SigningCredentials =
                        new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                };

            var token =
                tokenHandler.CreateToken(tokenDescriptor);

            // Xuất Token ra chuỗi chuẩn dạng Header.Payload.Signature
            return tokenHandler.WriteToken(token);
        }
    }
}
