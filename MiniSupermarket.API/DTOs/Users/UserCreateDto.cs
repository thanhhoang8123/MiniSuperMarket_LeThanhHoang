using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên không được để trống.")]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vai trò không được để trống.")]
        public string Role { get; set; } = "Cashier";
    }
}
