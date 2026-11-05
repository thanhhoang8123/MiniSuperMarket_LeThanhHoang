using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Mật khẩu mới không được để trống.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
