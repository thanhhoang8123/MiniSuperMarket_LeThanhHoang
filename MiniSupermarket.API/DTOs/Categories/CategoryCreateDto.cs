using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs
{
    public class CategoryCreateDto
    {
        [Required(ErrorMessage = "Tên nhóm hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên nhóm hàng tối đa 100 ký tự.")]
        public string CategoryName { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "Mô tả tối đa 255 ký tự.")]
        public string? Description { get; set; }
    }
}
